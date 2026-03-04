using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalFinanceTracker_Contracts.AiInsightsContracts;
using PFA_DTO.RequestModels;
using PFA_Exceptions.Exceptions;
using PFA_Services.HelperMethods;
using PFA_Services.ServiceProperties;
using Refit;
using StackExchange.Redis;

namespace PFA_Services.AIInsightService
{
    public class AIInsightService : IAIInsightService
    {
        private readonly IAiClient _aiClient;
        private readonly AiInsightSettings _aiInsightSettings;
        private readonly ILogger<AIInsightService> _logger;

        public AIInsightService(IAiClient aiClient, IOptions<AiInsightSettings> aiInsightSettings, ILogger<AIInsightService> logger)
        {
            _aiClient = aiClient;
            _aiInsightSettings = aiInsightSettings.Value;
            _logger = logger;
        }

        public async Task<string> GeneratePrompt<T>(T dataEntry)
        {
            return $"You are a financial analyst. Analyze the provided data and provide three " +
                $"actionable insights or observations for the user for his expenses and incomes. {dataEntry.ToJson()}";

        }

        public async Task<string> AskAsync(string prompt)
        {
            var requestBody = new AiInsightRequestModel()
            {
                Model = _aiInsightSettings.LLModel,
                Options = new AiOptions
                {
                    Temperature = 0.2
                },
                Prompt = prompt,
                Stream = false
            };
            try
            {
                var response = await _aiClient.GenerateAsync(requestBody);
                _logger.LogInformation("Insight generated!");
                return response.Response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Error while generating insight, {ex.Message}");
                throw new CoreException(ex.Message);
            }
        }

        public async Task SaveAiInsightInCache(FinancialSnapshot financialSnapshot, string aiInsight, IDatabase redisDb)
        {
            var key = $"{financialSnapshot.UserContractId}:ai_insight";

            var entries = new HashEntry[]
            {
                new HashEntry("Created", DateTime.UtcNow.ToString("o")),
                new HashEntry("Expires", DateTime.UtcNow.AddHours(24).ToString("o")),
                new HashEntry("Value", aiInsight)
            };

            redisDb.HashSet(key, entries);
            redisDb.KeyExpire(key, TimeSpan.FromHours(24));
        }
    }
}
