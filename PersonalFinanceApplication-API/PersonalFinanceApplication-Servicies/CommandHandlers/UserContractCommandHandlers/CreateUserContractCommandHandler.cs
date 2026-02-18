using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.EventServices.BalanceEvent;

namespace PersonalFinanceApplication_Services.CommandHandlers.UserContractCommandHandlers
{
    public class CreateUserContractCommand : IRequest<int>
    {
        public UserContractDto UserContractDto { get; set; }
    }

    public class CreateUserContractValidator : AbstractValidator<CreateUserContractCommand>
    {
        public CreateUserContractValidator()
        {
            RuleFor(userContract => userContract.UserContractDto.AccountBalance).NotNull().NotEmpty();
            RuleFor(userContract => userContract.UserContractDto.ContractName).NotNull().NotEmpty();
            RuleFor(userContract => userContract.UserContractDto.ContractType).IsInEnum().NotNull().NotEmpty();
            RuleFor(userContract => userContract.UserContractDto.DateOpened).NotNull().NotEmpty();
            RuleFor(userContract => userContract.UserContractDto.UserContractStatus).IsInEnum().NotNull().NotEmpty();
        }
    }

    public class CreateUserContractCommandHandler : IRequestHandler<CreateUserContractCommand, int>
    {
        private readonly IUserContractRepository _userContractRepository;
        private readonly IBalanceEventService _balanceEventService;
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        public CreateUserContractCommandHandler(IUserContractRepository userContractRepository, IBalanceEventService balanceEventService, IAccountBalanceRepository accountBalanceRepository)
        {
            _userContractRepository = userContractRepository;
            _balanceEventService = balanceEventService;
            _accountBalanceRepository = accountBalanceRepository;
        }

        public async Task<int> Handle(CreateUserContractCommand request, CancellationToken cancellationToken)
        {
            var userContract = request.UserContractDto.ToModel();
            var userContractId = _userContractRepository.AddEntity(userContract);
            SaveAccountBalanceToUserContract(request, userContractId);
            request.UserContractDto.UserContractId = userContractId;

            await _balanceEventService.InitializeBalanceOnContractCreation(request.UserContractDto,
                request.UserContractDto.AccountBalance, BalanceOperation.InitializeBalance);

            return userContract.UserContractId;
        }

        private void SaveAccountBalanceToUserContract(CreateUserContractCommand request, int userContractId)
        {
            var accountBalance = new AccountBalance()
            {
                Amount = request.UserContractDto.AccountBalance.Amount,
                Currency = request.UserContractDto.AccountBalance.Currency,
                IsActive = request.UserContractDto.AccountBalance.IsActive,
                LastDateAddedMoney = request.UserContractDto.AccountBalance.LastDateAddedMoney,
                LastDateDrawMoney = request.UserContractDto.AccountBalance.LastDateDrawMoney,
                UserContractId = userContractId
            };
            _accountBalanceRepository.AddEntity(accountBalance);
        }
    }
}
