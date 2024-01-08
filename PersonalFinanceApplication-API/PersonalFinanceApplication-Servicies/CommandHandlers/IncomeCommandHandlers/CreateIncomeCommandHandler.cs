using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Mappers.IncomeMappers;

namespace PersonalFinanceApplication_Services.CommandHandlers.IncomeCommandHandlers
{
    public class CreateIncomeCommand : IRequest<int>
    {
        public IncomeDto IncomeDto { get; set; }
    }

    public class CreateIncomeValidator : AbstractValidator<CreateIncomeCommand>
    {
        public CreateIncomeValidator()
        {
            RuleFor(income => income.IncomeDto.Amount).NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Account).IsInEnum().NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Category).IsInEnum().NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Purpose).NotEmpty().NotNull();
        }
    }

    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, int>
    {
        private readonly IIncomeRepository _incomeRepository;
        public CreateIncomeCommandHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<int> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateIncomeValidator();
            validator.ValidateAndThrow(request);

            var income = request.IncomeDto.ToModel();
            _incomeRepository.AddEntity(income);

            return income.IncomeId;
        }
    }
}
