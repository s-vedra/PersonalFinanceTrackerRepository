using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.HelperMethods;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceApplication_Services.QueryHandlers.IncomeQueryHandlers
{
    public class GetIncomesQuery : IRequest<List<IncomeDto>>
    {
        public int UserContractId { get; set; }
    }

    public class GetIncomesQueryValidator : AbstractValidator<GetIncomesQuery>
    {
        public GetIncomesQueryValidator()
        {
            RuleFor(userContract => userContract.UserContractId).NotNull().NotEmpty();
        }
    }
    public class GetIncomesQueryHandler : IRequestHandler<GetIncomesQuery, List<IncomeDto>>
    {
        private readonly IIncomeRepository _incomeRepository;
        public GetIncomesQueryHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<List<IncomeDto>> Handle(GetIncomesQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetIncomesQueryValidator();
            validator.ValidateAndThrow(request);
            var incomes = _incomeRepository.GetIncomesPerUserContract(request.UserContractId);
            if (!incomes.Any() || incomes.IsNull())
                throw new CoreException("No incomes found!");
            return incomes.Select(x => x.ToDto()).ToList();
        }
    }
}
