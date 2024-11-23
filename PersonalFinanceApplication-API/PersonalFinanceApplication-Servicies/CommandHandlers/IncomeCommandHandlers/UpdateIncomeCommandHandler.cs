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
            RuleFor(income => income.IncomeDto.Currency).NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.PaymentIssue).IsInEnum().NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Category).IsInEnum().NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Purpose).NotEmpty().NotNull();
        }
    }

    public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IBalanceEventService _balanceEventService;
        private readonly IUserContractRepository _userContractRepository;
        public UpdateIncomeCommandHandler(IIncomeRepository incomeRepository, IBalanceEventService balanceEventService,
            IUserContractRepository userContractRepository)
        {
            _incomeRepository = incomeRepository;
            _balanceEventService = balanceEventService;
            _userContractRepository = userContractRepository;
        }
        public async Task Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateIncomeValidator();
            validator.ValidateAndThrow(request);
            var userContract = _userContractRepository.GetEntity(request.IncomeDto.UserContractId);
            var income = _incomeRepository.GetEntity(request.IncomeDto.IncomeId);
            if (!userContract.IsNull() && !income.IsNull())
            {
                var updatedIncome = request.IncomeDto.ToModel();
                _incomeRepository.UpdateEntity(income, updatedIncome);
                await _balanceEventService.AdjustBalanceOnRecievedIncome(userContract.ToDto(), request.IncomeDto,
                TransactionType.Income, BalanceOperation.AdjustBalance);
            }
            else
            {
                throw new CoreException("No income or user contract found");
            }
        }
    }
}
