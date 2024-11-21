using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_DTO.NotificationModels;
using PersonalFinanceApplication_Mappers.Mappers;

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
            RuleFor(income => income.IncomeDto.Account).IsInEnum().NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Category).IsInEnum().NotEmpty().NotNull();
            RuleFor(income => income.IncomeDto.Purpose).NotEmpty().NotNull();
        }
    }

    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, int>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IUserContractRepository _userContractRepository;
        private readonly IMediator _mediator;
        public CreateIncomeCommandHandler(IIncomeRepository incomeRepository,
            IUserContractRepository userContractRepository,
            IMediator mediator)
        {
            _incomeRepository = incomeRepository;
            _userContractRepository = userContractRepository;
            _mediator = mediator;
        }

        public async Task<int> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var userContract = _userContractRepository.GetEntity(request.IncomeDto.UserContractId);
            var validator = new CreateIncomeValidator();
            validator.ValidateAndThrow(request);

            var income = request.IncomeDto.ToModel();
            _incomeRepository.AddEntity(income);
            await PublishBalanceChangedEvent(userContract, income);

            return income.IncomeId;
        }

        private async Task PublishBalanceChangedEvent(UserContract userContract, Income income)
        {
            var notification = new BalanceChangedEvent()
            {
                UserContract = userContract.ToDto(),
                Account = income.Account,
                Amount = income.Amount,
                IncomeCategory = income.Category,
                Date = income.Date,
                Income = income.ToDto(),
                UserId = userContract.UserId,
                TransactionType = TransactionType.Income
            };
            await _mediator.Publish(notification);
        }
    }
}
