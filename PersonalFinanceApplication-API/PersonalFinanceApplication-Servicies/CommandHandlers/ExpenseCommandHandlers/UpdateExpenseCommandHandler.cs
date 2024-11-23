using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.EventServices.BalanceEvent;
using PersonalFinanceApplication_Services.ExtensionMethods;

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
        public UpdateExpenseCommandHandler(IExpenseRepository expenseRepository, IUserContractRepository userContractRepository,
            IBalanceEventService balanceEventService)
        {
            _expenseRepository = expenseRepository;
            _userContractRepository = userContractRepository;
            _balanceEventService = balanceEventService;
        }
        public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateExpenseValidator();
            validator.ValidateAndThrow(request);
            var userContract = _userContractRepository.GetEntity(request.ExpenseDto.UserContractId);
            var expense = _expenseRepository.GetEntity(request.ExpenseDto.ExpenseId);
            if (!userContract.IsNull() && !expense.IsNull())
            {
                var updatedExpense = request.ExpenseDto.ToModel();
                _expenseRepository.UpdateEntity(expense, updatedExpense);
                await _balanceEventService.AdjustBalanceOnRecievedExpense(userContract.ToDto(), request.ExpenseDto,
                TransactionType.Expense, BalanceOperation.AdjustBalance);
            }
            else
            {
                throw new CoreException("No expense or user contract found");
            }
        }
    }
}
