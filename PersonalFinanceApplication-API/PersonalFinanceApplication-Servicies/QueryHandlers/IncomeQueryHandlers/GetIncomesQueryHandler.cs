using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.ExtensionMethods;

namespace PersonalFinanceApplication_Services.QueryHandlers.IncomeAndBalanceQueryHandlers
{
    public class GetIncomesQuery : IRequest<List<IncomeDto>>
    {
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
            var incomes = _incomeRepository.GetAllEntities();
            if (!incomes.Any() || incomes.IsNull())
                throw new CoreException("No incomes found!");
            return incomes.Select(x => x.ToDto()).ToList();
        }
    }
}
