using MassTransit;
using PersonalFinanceTracker_Contracts.AiInsightsContracts;
using PFA_Services.AIInsightService;
using PFA_Services.HelperMethods;

namespace PFA_Services.ConsumerService
{
    public class AiInsightConsumer : IConsumer<FinancialSnapshot>
    {
        private readonly IAIInsightService _aiInsightService;
        public AiInsightConsumer(IAIInsightService aiInsightService)
        {
            _aiInsightService = aiInsightService;
        }
        public async Task Consume(ConsumeContext<FinancialSnapshot> context)
        {
            var prompt = $"You are a financial analyst. Analyze the provided data and provide three actionable insights or observations for the user for his expenses and incomes. {context.Message.ToJson()}";
            var test = await _aiInsightService.AskAsync(prompt);
            Console.WriteLine(test);
        }
    }
}
