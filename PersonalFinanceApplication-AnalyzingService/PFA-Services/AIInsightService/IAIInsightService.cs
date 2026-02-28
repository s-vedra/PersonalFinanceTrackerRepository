namespace PFA_Services.AIInsightService
{
    public interface IAIInsightService
    {
        Task<string> AskAsync(string prompt);
    }
}
