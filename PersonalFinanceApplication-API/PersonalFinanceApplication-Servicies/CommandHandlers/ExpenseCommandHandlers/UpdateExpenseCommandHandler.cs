using Azure.Core;
using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DAL.Implementation;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.EventServices.BalanceEvent;
using PersonalFinanceApplication_Services.HelperMethods;

namespace PersonalFinanceApplication_Services.CommandHandlers.ExpenseCommandHandlers
{
    public class UpdateExpenseCommand : IRequest
    {
        public ExpenseDto ExpenseDto { get; set; }
    }

    public class UpdateExpenseValidator : AbstractValidator<UpdateExpenseCommand>
    {
        public UpdateExpenseValidator()
        {
            RuleFor(expense => expense.ExpenseDto.ExpenseId).NotNull().NotEmpty();
            RuleFor(expense => expense.ExpenseDto.ReferenceId).NotNull().NotEmpty();
            RuleFor(expense => expense.ExpenseDto.Amount).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(expense => expense.ExpenseDto.Currency).NotEmpty().NotNull();
            RuleFor(expense => expense.ExpenseDto.PaymentIssue).IsInEnum().NotEmpty().NotNull();
            RuleFor(expense => expense.ExpenseDto.Category).IsInEnum().NotEmpty().NotNull();
            RuleFor(expense => expense.ExpenseDto.Purpose).NotEmpty().NotNull();
        }
    }

    public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserContractRepository _userContractRepository;
        private readonly IBalanceEventService _balanceEventService;
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        public UpdateExpenseCommandHandler(IExpenseRepository expenseRepository, IUserContractRepository userContractRepository,
            IBalanceEventService balanceEventService, IAccountBalanceRepository accountBalanceRepository)
        {
            _expenseRepository = expenseRepository;
            _userContractRepository = userContractRepository;
            _balanceEventService = balanceEventService;
            _accountBalanceRepository = accountBalanceRepository;
        }
        public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateExpenseValidator();
            validator.ValidateAndThrow(request);
            var userContract = _userContractRepository.GetEntity(request.ExpenseDto.UserContractId);
            var expense = _expenseRepository.GetEntity(request.ExpenseDto.ReferenceId);
            if (!userContract.IsNull() && !expense.IsNull())
            {
                await UpdateExpenseAsync(request.ExpenseDto, expense, userContract);
                await _balanceEventService.AdjustBalanceOnRecievedExpense(userContract.ToDto(), request.ExpenseDto,
                TransactionType.Expense, BalanceOperation.AdjustBalance);
            }
            else
            {
                throw new CoreException("No expense or user contract found");
            }
        }

        private async Task UpdateExpenseAsync(ExpenseDto expenseDto, Expense expense, UserContract userContract)
        {
            var updatedExpense = expenseDto.ToModel();
            var accountBalance = _accountBalanceRepository.GetEntity(userContract.UserContractId);
            accountBalance.Amount += expense.Amount;
            _accountBalanceRepository.UpdateEntity(accountBalance, accountBalance);
            _expenseRepository.UpdateEntity(expense, updatedExpense);
        }
    }
}
