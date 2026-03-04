using MassTransit;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceTracker_Contracts.AiInsightsContracts;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;
using StackExchange.Redis;

namespace PersonalFinanceApplication_Services.EventServices.AiInsightEvent
{
    public class AiInsightEventService : IAiInsightEventService
    {
        private readonly IBus _bus;
        private readonly IDatabase _redisDb;
        public AiInsightEventService(IBus bus, IConnectionMultiplexer redisDb)
        {
            _bus = bus;
            _redisDb = redisDb.GetDatabase(); 
        }

        public async Task EnsureAiInsightCachedAsync(UserContractSummaryDto userContractSummaryDto, SalarySchedulerDto salarySchedulerDto)
        {
            var key = $"{userContractSummaryDto.UserContractId}:ai_insight";
            if (await _redisDb.KeyExistsAsync(key))
            {
                var insightValue = await _redisDb.HashGetAsync(key, "Value");
                userContractSummaryDto.AIInsight = insightValue.HasValue
                    ? insightValue.ToString()
                    : "Generating insight...";
            }
            else
            {
                var financialSnapshot = CreateFinancialSnapshot(userContractSummaryDto, salarySchedulerDto);
                _bus.Publish(financialSnapshot);
                userContractSummaryDto.AIInsight = "Generating insight...";
            }
        }

        private FinancialSnapshot CreateFinancialSnapshot(UserContractSummaryDto userContractSummaryDto, SalarySchedulerDto salarySchedulerDto)
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            var financialSnapshot = new FinancialSnapshot()
            {
                UserId = userContractSummaryDto.UserId,
                UserContractId = userContractSummaryDto.UserContractId,
                Expenses = userContractSummaryDto.Expenses.Where(x => x.Date.Year == DateTime.Now.Year && x.Date.Month == lastMonth.Month),
                Incomes = userContractSummaryDto.Incomes.Where(x => x.Date.Year == DateTime.Now.Year && x.Date.Month == lastMonth.Month),
                Period = $"Month - {lastMonth.ToString("MMMM")}",
                TotalAmountOnAccount = userContractSummaryDto.AccountBalance.Amount,
                TotalSalary = salarySchedulerDto.Amount,
                Currency = userContractSummaryDto.AccountBalance.Currency
            };

            return financialSnapshot;
        }
    }
}
