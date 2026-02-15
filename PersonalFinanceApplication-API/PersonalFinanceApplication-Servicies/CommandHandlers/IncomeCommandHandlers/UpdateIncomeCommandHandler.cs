using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DAL.Implementation;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.EventServices.BalanceEvent;
using PersonalFinanceApplication_Services.HelperMethods;

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
            RuleFor(expense => expense.IncomeDto.ReferenceId).NotNull().NotEmpty();
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
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        public UpdateIncomeCommandHandler(IIncomeRepository incomeRepository, IBalanceEventService balanceEventService,
            IUserContractRepository userContractRepository, IAccountBalanceRepository accountBalanceRepository)
        {
            _incomeRepository = incomeRepository;
            _balanceEventService = balanceEventService;
            _userContractRepository = userContractRepository;
            _accountBalanceRepository = accountBalanceRepository;
        }
        public async Task Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateIncomeValidator();
            validator.ValidateAndThrow(request);
            var userContract = _userContractRepository.GetEntity(request.IncomeDto.UserContractId);
            var income = _incomeRepository.GetEntity(request.IncomeDto.ReferenceId);
            if (!userContract.IsNull() && !income.IsNull())
            {
                await UpdateIncomeAsync(request.IncomeDto, income, userContract);
                await _balanceEventService.AdjustBalanceOnRecievedIncome(userContract.ToDto(), request.IncomeDto,
               TransactionType.Income, BalanceOperation.AdjustBalance);
            }
            else
            {
                throw new CoreException("No income or user contract found");
            }
        }

        private async Task UpdateIncomeAsync(IncomeDto incomeDto, Income income, UserContract userContract)
        {
            var updateIncome = incomeDto.ToModel();
            var accountBalance = _accountBalanceRepository.GetEntity(userContract.UserContractId);
            accountBalance.Amount -= income.Amount;
            _accountBalanceRepository.UpdateEntity(accountBalance, accountBalance);
            _incomeRepository.UpdateEntity(income, updateIncome);
        }
    }
}
