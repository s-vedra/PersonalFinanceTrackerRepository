using MassTransit;
using PersonalFinanceTracker_Contracts.AiInsightsContracts;
using PFA_Services.AIInsightService;
using PFA_Services.FinancialSummaryCalculationService;
using StackExchange.Redis;

namespace PFA_Services.ConsumerService
{
    public class AiInsightConsumer : IConsumer<FinancialSnapshot>
    {
        private readonly IAIInsightService _aiInsightService;
        private readonly IFinancialSummaryCalculationService _financialSummaryCalculationService;
        private readonly IDatabase _redisDb;
        public AiInsightConsumer(IAIInsightService aiInsightService, IFinancialSummaryCalculationService financialSummaryCalculationService, IConnectionMultiplexer redisDb)
        {
            _aiInsightService = aiInsightService;
            _financialSummaryCalculationService = financialSummaryCalculationService;
            _redisDb = redisDb.GetDatabase();
        }
        public async Task Consume(ConsumeContext<FinancialSnapshot> context)
        {
            var financialSnapshotCalculation = _financialSummaryCalculationService.CalculateFinancialSnapshotPrompt(context.Message);
            var prompt = await _aiInsightService.GeneratePrompt(financialSnapshotCalculation);
            var aiInsight = await _aiInsightService.AskAsync(prompt);
            await _aiInsightService.SaveAiInsightInCache(context.Message, aiInsight, _redisDb);
        }
    }
}
