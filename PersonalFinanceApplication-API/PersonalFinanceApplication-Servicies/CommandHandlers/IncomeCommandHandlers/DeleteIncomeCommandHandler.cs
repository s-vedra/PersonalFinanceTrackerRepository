using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.EventServices.BalanceEvent;
using PersonalFinanceApplication_Services.HelperMethods;

namespace PersonalFinanceApplication_Services.CommandHandlers.IncomeCommandHandlers
{
    public class DeleteIncomeCommand : IRequest
    {
        public Guid ReferenceId { get; set; }
    }

    public class DeleteIncomeValidator : AbstractValidator<DeleteIncomeCommand>
    {
        public DeleteIncomeValidator()
        {
            RuleFor(x => x.ReferenceId).NotNull().NotEmpty();
        }
    }

    public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IBalanceEventService _balanceEventService;
        private readonly IUserContractRepository _userContractRepository;
        public DeleteIncomeCommandHandler(IIncomeRepository incomeRepository, IBalanceEventService balanceEventService, IUserContractRepository userContractRepository)
        {
            _incomeRepository = incomeRepository;
            _balanceEventService = balanceEventService;
            _userContractRepository = userContractRepository;
        }

        public async Task Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteIncomeValidator();
            validator.ValidateAndThrow(request);

            var income = _incomeRepository.GetEntity(request.ReferenceId);
            var userContract = _userContractRepository.GetEntity(income.UserContractId);
            var expense = new ExpenseDto()
            {
                Amount = income.Amount,
                UserContractId = income.UserContractId,
                Currency = income.Currency,
                Date = income.Date
            };
            if (!income.IsNull() && !userContract.IsNull())
            {
                _incomeRepository.DeleteEntity(income);
                await _balanceEventService.AdjustBalanceOnRecievedExpense(userContract.ToDto(), expense,
                        TransactionType.Expense, BalanceOperation.AdjustBalance);
            }
            else
            {
                throw new CoreException("No income found");
            }
        }
    }
}
