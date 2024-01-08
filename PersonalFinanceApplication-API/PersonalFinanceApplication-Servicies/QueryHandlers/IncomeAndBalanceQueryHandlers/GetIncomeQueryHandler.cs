using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.IncomeMappers;

namespace PersonalFinanceApplication_Services.QueryHandlers.IncomeAndBalanceQueryHandlers
{
    public class GetIncomeQuery : IRequest<IncomeDto>
    {
        public int Id { get; set; }
    }

    public class GetIncomeValidator : AbstractValidator<GetIncomeQuery>
    {
        public GetIncomeValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }

    public class GetIncomeQueryHandler : IRequestHandler<GetIncomeQuery, IncomeDto>
    {
        private readonly IIncomeRepository _incomeRepository;
        public GetIncomeQueryHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<IncomeDto> Handle(GetIncomeQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetIncomeValidator();
            validator.ValidateAndThrow(request);

            var income = _incomeRepository.GetEntity(request.Id);
            if (income != null)
                return income.ToDto();
            throw new CoreException("No income found");
        }
    }
}
