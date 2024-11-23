using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Enums;
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
        public CreateUserContractCommandHandler(IUserContractRepository userContractRepository, IBalanceEventService balanceEventService)
        {
            _userContractRepository = userContractRepository;
            _balanceEventService = balanceEventService;
        }

        public async Task<int> Handle(CreateUserContractCommand request, CancellationToken cancellationToken)
        {
            var userContract = request.UserContractDto.ToModel();
            var userContractId = _userContractRepository.AddEntity(userContract);

            request.UserContractDto.UserContractId = userContractId;

            await _balanceEventService.InitializeBalanceOnContractCreation(request.UserContractDto,
                request.UserContractDto.AccountBalance, BalanceOperation.InitializeBalance);

            return userContract.UserContractId;
        }
    }
}
