using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Services.ExtensionMethods;

namespace PersonalFinanceApplication_Services.CommandHandlers
{
    public class DeleteExpenseCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteExpenseValidator : AbstractValidator<DeleteExpenseCommand>
    {
        public DeleteExpenseValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }

    public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        public DeleteExpenseCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteExpenseValidator();
            validator.ValidateAndThrow(request);

            var expense = _expenseRepository.GetEntity(request.Id);
            if (!expense.IsNull())
            {
                _expenseRepository.DeleteEntity(expense);
            }
            else
            {
                throw new CoreException("No expense found");
            }
        }
    }
}
