using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.HelperMethods;

namespace PersonalFinanceApplication_Services.QueryHandlers.ExpenseQueryHandlers
{
    public class GetExpenseQuery : IRequest<ExpenseDto>
    {
        public Guid ReferenceId { get; set; }
    }

    public class GetExpenseValidator : AbstractValidator<GetExpenseQuery>
    {
        public GetExpenseValidator()
        {
            RuleFor(x => x.ReferenceId).NotNull().NotEmpty();
        }
    }

    public class GetExpenseQueryHandler : IRequestHandler<GetExpenseQuery, ExpenseDto>
    {
        private readonly IExpenseRepository _expenseRepository;
        public GetExpenseQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<ExpenseDto> Handle(GetExpenseQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetExpenseValidator();
            validator.ValidateAndThrow(request);

            var expense = _expenseRepository.GetEntity(request.ReferenceId);
            if (!expense.IsNull())
                return expense.ToDto();
            throw new CoreException("No expense found");
        }
    }
}
