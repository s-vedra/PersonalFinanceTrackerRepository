using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.EventServices.BalanceEvent;
using PersonalFinanceApplication_Services.HelperMethods;

namespace PersonalFinanceApplication_Services.CommandHandlers.ExpenseCommandHandlers
{
    public class DeleteExpenseCommand : IRequest
    {
        public Guid ReferenceId { get; set; }
    }

    public class DeleteExpenseValidator : AbstractValidator<DeleteExpenseCommand>
    {
        public DeleteExpenseValidator()
        {
            RuleFor(x => x.ReferenceId).NotNull().NotEmpty();
        }
    }

    public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IBalanceEventService _balanceEventService;
        private readonly IUserContractRepository _userContractRepository;
        public DeleteExpenseCommandHandler(IExpenseRepository expenseRepository, IBalanceEventService balanceEventService, IUserContractRepository userContractRepository)
        {
            _expenseRepository = expenseRepository;
            _balanceEventService = balanceEventService;
            _userContractRepository = userContractRepository;
        }

        public async Task Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteExpenseValidator();
            validator.ValidateAndThrow(request);

            var expense = _expenseRepository.GetEntity(request.ReferenceId);
            var userContract = _userContractRepository.GetEntity(expense.UserContractId);
            var income = new IncomeDto()
            {
                Amount = expense.Amount,
                UserContractId = expense.UserContractId,
                Currency = expense.Currency,
                Date = expense.Date
            };
            if (!expense.IsNull() && !userContract.IsNull())
            {
                _expenseRepository.DeleteEntity(expense);
                await _balanceEventService.AdjustBalanceOnRecievedIncome(userContract.ToDto(), income,
                    TransactionType.Income, BalanceOperation.AdjustBalance);
            }
            else
            {
                throw new CoreException("No expense found");
            }
        }
    }
}
