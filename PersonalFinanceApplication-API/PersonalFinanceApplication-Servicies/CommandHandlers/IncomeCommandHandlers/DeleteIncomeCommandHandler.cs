using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Services.ExtensionMethods;

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
            if (!income.IsNull())
            {
                _incomeRepository.DeleteEntity(income);
            }
            else
            {
                throw new CoreException("No income found");
            }
        }
    }
}
