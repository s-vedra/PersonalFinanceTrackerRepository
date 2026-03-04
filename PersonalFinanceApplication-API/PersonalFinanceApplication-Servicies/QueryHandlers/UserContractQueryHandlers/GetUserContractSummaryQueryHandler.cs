using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.EventServices.AiInsightEvent;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceApplication_Services.QueryHandlers.UserContractQueryHandlers
{
    public class GetUserContractSummaryQuery : IRequest<UserContractSummaryDto>
    {
        public int UserContractId { get; set; }
    }

    public class GetUserContractSummaryQueryHandler : IRequestHandler<GetUserContractSummaryQuery, UserContractSummaryDto>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IIncomeRepository _incomeRepository;
        private readonly IUserContractRepository _userContractRepository;
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        private readonly IScheduledSalaryRepository _scheduleSalaryRepository;
        private readonly IAiInsightEventService _aiInsightEventService;

        public GetUserContractSummaryQueryHandler(IExpenseRepository expenseRepository, IIncomeRepository incomeRepository, IUserContractRepository userContractRepository,
            IAccountBalanceRepository accountBalanceRepository, IScheduledSalaryRepository scheduledSalaryRepository, IAiInsightEventService aiInsightEventService)
        {
            _expenseRepository = expenseRepository;
            _incomeRepository = incomeRepository;
            _userContractRepository = userContractRepository;
            _accountBalanceRepository = accountBalanceRepository;
            _scheduleSalaryRepository = scheduledSalaryRepository;
            _aiInsightEventService = aiInsightEventService;
        }

        public async Task<UserContractSummaryDto> Handle(GetUserContractSummaryQuery request, CancellationToken cancellationToken)
        {
            var userContract = _userContractRepository.GetEntity(request.UserContractId);
            if (userContract == null)
                throw new CoreException("No User Contract found!");

            var accountBalance = _accountBalanceRepository.GetEntity(request.UserContractId);
            var userIncomes = _incomeRepository.GetIncomesPerUserContract(userContract.UserContractId) ?? Enumerable.Empty<Income>();
            var userExpenditures = _expenseRepository.GetExpendituresPerUserContract(userContract.UserContractId) ?? Enumerable.Empty<Expense>();
            var salaryScheduler = _scheduleSalaryRepository.GetSalarySchedulerPerUserContract(userContract.UserContractId);

            var userContractSummaryDto = new UserContractSummaryDto
            {
                UserContractId = userContract.UserContractId,
                ContractType = (ContractType)userContract.ContractType,
                ContractName = userContract.ContractName,
                Created = userContract.Created,
                DateOpened = userContract.DateOpened,
                IsActive = userContract.IsActive,
                UserContractStatus = (UserContractStatus)userContract.UserContractStatus,
                UserId = userContract.UserId,
                AccountBalance = accountBalance?.ToDto(),
                Expenses = userExpenditures.Select(x => x.ToDto()).ToList(),
                Incomes = userIncomes.Select(x => x.ToDto()).ToList()
                //add salary scheduler also
            };

            var salarySchedulerDto = salaryScheduler?.ToDto();
            await _aiInsightEventService.EnsureAiInsightCachedAsync(userContractSummaryDto, salarySchedulerDto);

            return userContractSummaryDto;
        }
    }
}
