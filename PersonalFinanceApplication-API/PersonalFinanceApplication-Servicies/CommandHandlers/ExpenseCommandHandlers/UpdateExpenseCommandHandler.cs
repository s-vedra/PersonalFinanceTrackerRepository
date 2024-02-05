using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.ExpenseMapper;

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
            RuleFor(expense => expense.ExpenseDto.Account).IsInEnum().NotEmpty().NotNull();
            RuleFor(expense => expense.ExpenseDto.Category).IsInEnum().NotEmpty().NotNull();
            RuleFor(expense => expense.ExpenseDto.Purpose).NotEmpty().NotNull();
        }
    }

    public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        public UpdateExpenseCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateExpenseValidator();
            validator.ValidateAndThrow(request);

            var expense = _expenseRepository.GetEntity(request.ExpenseDto.ExpenseId);

            if (expense != null)
            {
                var updatedExpense = request.ExpenseDto.ToModel();
                _expenseRepository.UpdateEntity(expense, updatedExpense);
            }
            else
            {
                throw new CoreException("No expense found");
            }
        }
    }
}
