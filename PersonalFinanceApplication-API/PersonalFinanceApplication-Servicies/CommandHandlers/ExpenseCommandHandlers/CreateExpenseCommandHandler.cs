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
    public class CreateExpenseCommand : IRequest<Guid>
    {
        public ExpenseDto ExpenseDto { get; set; }
    }

    public class CreateExpenseValidator : AbstractValidator<CreateExpenseCommand>
    {
        public CreateExpenseValidator()
        {
            RuleFor(expense => expense.ExpenseDto.Amount).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(expense => expense.ExpenseDto.Currency).NotEmpty().NotNull();
            RuleFor(expense => expense.ExpenseDto.PaymentIssue).IsInEnum().NotEmpty().NotNull();
            RuleFor(expense => expense.ExpenseDto.Category).IsInEnum().NotEmpty().NotNull();
            RuleFor(expense => expense.ExpenseDto.Purpose).NotEmpty().NotNull();
        }
    }

    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Guid>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserContractRepository _userContractRepository;
        private readonly IBalanceEventService _balanceEventService;
        public CreateExpenseCommandHandler(IExpenseRepository expenseRepository, IUserContractRepository userContractRepository,
            IBalanceEventService balanceEventService)
        {
            _expenseRepository = expenseRepository;
            _userContractRepository = userContractRepository;
            _balanceEventService = balanceEventService;
        }

        public async Task<Guid> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExpenseValidator();
            validator.ValidateAndThrow(request);
            var userContract = _userContractRepository.GetEntity(request.ExpenseDto.UserContractId);
            if (!userContract.IsNull())
            {
                var expense = request.ExpenseDto.ToModel();
                _expenseRepository.AddEntity(expense);
                await _balanceEventService.AdjustBalanceOnRecievedExpense(userContract.ToDto(), request.ExpenseDto,
                    TransactionType.Expense, BalanceOperation.AdjustBalance);

                return expense.ReferenceId;
            }
            throw new CoreException("User contract cannot be found");
        }
    }
}
