using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.HelperMethods;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceApplication_Services.QueryHandlers.ExpenseQueryHandlers
{
    public class GetExpensesQuery : IRequest<List<ExpenseDto>>
    {
        public int UserContractId { get; set; }
    }

    public class GetExpensesQueryValidator : AbstractValidator<GetExpensesQuery>
    {
        public GetExpensesQueryValidator()
        {
            RuleFor(userContract => userContract.UserContractId).NotNull().NotEmpty();
        }
    }

    public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, List<ExpenseDto>>
    {
        private readonly IExpenseRepository _expenseRepository;
        public GetExpensesQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<List<ExpenseDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetExpensesQueryValidator();
            validator.ValidateAndThrow(request);
            var expenses = _expenseRepository.GetExpendituresPerUserContract(request.UserContractId);
            if (!expenses.Any() || expenses.IsNull())
                throw new CoreException("No expenses found!");
            return expenses.Select(x => x.ToDto()).ToList();
        }
    }
}
