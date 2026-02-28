using MassTransit;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.HelperMethods;
using PersonalFinanceTracker_Contracts.AiInsightsContracts;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;
using StackExchange.Redis;

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
        private readonly IDatabase _redisDb;
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        private readonly IBus _bus;


        public GetUserContractSummaryQueryHandler(IExpenseRepository expenseRepository, IIncomeRepository incomeRepository, IUserContractRepository userContractRepository,
            IConnectionMultiplexer redisDb, IAccountBalanceRepository accountBalanceRepository, IBus bus)
        {
            _expenseRepository = expenseRepository;
            _incomeRepository = incomeRepository;
            _userContractRepository = userContractRepository;
            _redisDb = redisDb.GetDatabase();
            _accountBalanceRepository = accountBalanceRepository;
            _bus = bus;
        }

        public async Task<UserContractSummaryDto> Handle(GetUserContractSummaryQuery request, CancellationToken cancellationToken)
        {
            var userContract = _userContractRepository.GetEntity(request.UserContractId);
            if (!userContract.IsNull())
            {
                var accountBalance = _accountBalanceRepository.GetEntity(request.UserContractId);
                var userIncomes = _incomeRepository.GetIncomesPerUserContract(userContract.UserContractId);
                var userExpenditures = _expenseRepository.GetExpendituresPerUserContract(userContract.UserContractId);

                var userContractSummaryDto = new UserContractSummaryDto()
                {
                    UserContractId = userContract.UserContractId,
                    ContractType = (ContractType)userContract.ContractType,
                    ContractName = userContract.ContractName,
                    Created = userContract.Created,
                    DateOpened = userContract.DateOpened,
                    IsActive = userContract.IsActive,
                    UserContractStatus = (UserContractStatus)userContract.UserContractStatus,
                    UserId = userContract.UserId,
                    AccountBalance = accountBalance.ToDto(),
                    Expenses = userExpenditures.Select(x => x.ToDto()).ToList(),
                    Incomes = userIncomes.Select(x => x.ToDto()).ToList()
                };


                var test = new FinancialSnapshot()
                {
                    UserId = userContract.UserId,
                    UserContractId = userContract.UserContractId,
                    Expenses = userContractSummaryDto.Expenses.Where(x => x.Date.Year == DateTime.Now.Year && x.Date.Month == DateTime.Now.Month),
                    Incomes = userContractSummaryDto.Incomes.Where(x => x.Date.Year == DateTime.Now.Year && x.Date.Month == DateTime.Now.Month),
                    Period = $"Last 30 days - month {DateTime.Now.Month}",
                    TotalAmountOnAccount = userContractSummaryDto.AccountBalance.Amount,
                    TotalSalary = 560000
                };

                 _bus.Publish(test);
                //var key = $"{userContractSummaryDto.UserContractId}:ai_insight";
                //var cached = _redisDb.StringGet(key);
                //if (!string.IsNullOrEmpty(cached))
                //{
                //    userContractSummaryDto.AIInsight = cached;
                //}
                //else
                //{
                //    _redisDb.StringSet(key, "Test insight", TimeSpan.FromMinutes(5));
                //    userContractSummaryDto.AIInsight = "working on it";
                //}

                //var snapshot = new FinancialSnapshot
                //{
                //    Period = "Last30Days",
                //    TotalIncome = 5000,
                //    TotalExpenses = 3200,
                //    NetSavings = 1800,
                //    SavingsRate = 0.36,
                //    AverageExpenseAmount = 160,
                //    ExpenseVolatility = 50
                //};

                //snapshot.TopCategories.Add(new CategoryExpense
                //{
                //    Category = "Food",
                //    Amount = 1200,
                //    Percentage = 37.5
                //});
                //snapshot.TopCategories.Add(new CategoryExpense
                //{
                //    Category = "Entertainment",
                //    Amount = 800,
                //    Percentage = 25
                //});

                //snapshot.MonthlyTrend.Add(new MonthlyData
                //{
                //    Month = "January",
                //    Income = 5000,
                //    Expenses = 3200
                //});
                //snapshot.MonthlyTrend.Add(new MonthlyData
                //{
                //    Month = "February",
                //    Income = 5200,
                //    Expenses = 3000
                //});

                //var test = new FinancialInsightRequest
                //{
                //    UserId = "user123",
                //    Snapshot = snapshot
                //};

                //_producerService.PublishMessageToGenerateInsight(test);


                return userContractSummaryDto;
            }
            throw new CoreException("No User Contract found!");
        }
    }
}
