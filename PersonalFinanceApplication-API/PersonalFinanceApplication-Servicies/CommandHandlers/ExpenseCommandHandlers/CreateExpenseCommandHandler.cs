using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Mappers.ExpenseMapper;

namespace PersonalFinanceApplication_Services.CommandHandlers.ExpenseCommands
{
    public class CreateExpenseCommand : IRequest<int>
    {
        public ExpenseDto ExpenseDto { get; set; }
    }

    public class CreateExpenseValidator : AbstractValidator<CreateExpenseCommand>
    {
        public CreateExpenseValidator()
        {
            RuleFor(expense => expense.ExpenseDto.Amount).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(expense => expense.ExpenseDto.Account).IsInEnum().NotEmpty().NotNull();
            RuleFor(expense => expense.ExpenseDto.Category).IsInEnum().NotEmpty().NotNull();
            RuleFor(expense => expense.ExpenseDto.Purpose).NotEmpty().NotNull();
        }
    }

    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, int>
    {
        private readonly IExpenseRepository _expenseRepository;
        public CreateExpenseCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<int> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExpenseValidator();
            validator.ValidateAndThrow(request);

            var expense = request.ExpenseDto.ToModel();
            _expenseRepository.AddEntity(expense);

            return expense.ExpenseId;
        }
    }
}
