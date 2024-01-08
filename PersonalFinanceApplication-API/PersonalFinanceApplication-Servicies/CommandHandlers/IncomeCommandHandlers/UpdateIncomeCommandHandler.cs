using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.IncomeMappers;

namespace PersonalFinanceApplication_Services.CommandHandlers.IncomeCommandHandlers
{
    public class UpdateIncomeCommand : IRequest
    {
        public IncomeDto IncomeDto { get; set; }
    }

    public class UpdateIncomeValidator : AbstractValidator<UpdateIncomeCommand>
    {
        public UpdateIncomeValidator()
        {
            RuleFor(income => income.IncomeDto.IncomeId).NotNull().NotEmpty();
            RuleFor(income => income.IncomeDto.Amount).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(income => income.IncomeDto.Account).IsInEnum().NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Category).IsInEnum().NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Purpose).NotEmpty().NotNull();
        }
    }

    public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand>
    {
        private readonly IIncomeRepository _incomeRepository;
        public UpdateIncomeCommandHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }
        public async Task Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateIncomeValidator();
            validator.ValidateAndThrow(request);

            var income = _incomeRepository.GetEntity(request.IncomeDto.IncomeId);

            if (income != null)
            {
                var updatedIncome = request.IncomeDto.ToModel();
                _incomeRepository.UpdateEntity(income, updatedIncome);
            }
            else
            {
                throw new CoreException("No income found");
            }
        }
    }
}
