using PersonalFinanceTracker_Contracts.AiInsightsContracts;
using StackExchange.Redis;

namespace PFA_Services.AIInsightService
{
    public interface IAIInsightService
    {
        Task<string> AskAsync(string prompt);
        Task<string> GeneratePrompt<T>(T dataEntry);
        Task SaveAiInsightInCache(FinancialSnapshot financialSnapshot, string aiInsight, IDatabase redisDb);
    }
}
