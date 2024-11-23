using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.EventServices.BalanceEvent;
using PersonalFinanceApplication_Services.ExtensionMethods;

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
            RuleFor(income => income.IncomeDto.Currency).NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.PaymentIssue).IsInEnum().NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Category).IsInEnum().NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Purpose).NotEmpty().NotNull();
        }
    }

    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, int>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IUserContractRepository _userContractRepository;
        private readonly IBalanceEventService _balanceEventService;
        public CreateIncomeCommandHandler(IIncomeRepository incomeRepository,
            IUserContractRepository userContractRepository,
            IBalanceEventService balanceEventService)
        {
            _incomeRepository = incomeRepository;
            _userContractRepository = userContractRepository;
            _balanceEventService = balanceEventService;
        }

        public async Task<int> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateIncomeValidator();
            validator.ValidateAndThrow(request);
            var userContract = _userContractRepository.GetEntity(request.IncomeDto.UserContractId);
            if (!userContract.IsNull())
            {
                var income = request.IncomeDto.ToModel();
                _incomeRepository.AddEntity(income);
                await _balanceEventService.AdjustBalanceOnRecievedIncome(userContract.ToDto(), request.IncomeDto,
                    TransactionType.Income, BalanceOperation.AdjustBalance);
                return income.IncomeId;
            }
            throw new CoreException("User contract cannot be found");
        }
    }
}
