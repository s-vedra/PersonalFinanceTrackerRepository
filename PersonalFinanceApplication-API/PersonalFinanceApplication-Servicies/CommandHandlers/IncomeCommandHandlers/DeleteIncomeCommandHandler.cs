using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;

namespace PersonalFinanceApplication_Services.CommandHandlers.IncomeCommandHandlers
{
    public class DeleteIncomeCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteIncomeValidator : AbstractValidator<DeleteIncomeCommand>
    {
        public DeleteIncomeValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }

    public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand>
    {
        private readonly IIncomeRepository _incomeRepository;
        public DeleteIncomeCommandHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteIncomeValidator();
            validator.ValidateAndThrow(request);

            var income = _incomeRepository.GetEntity(request.Id);
            _incomeRepository.DeleteEntity(income);
        }
    }
}
