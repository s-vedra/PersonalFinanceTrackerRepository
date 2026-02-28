using System.Net.Http.Json;

namespace PFA_Services.AIInsightService
{
    public class AIInsightService : IAIInsightService
    {
        private readonly HttpClient client;

        public AIInsightService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> AskAsync(string prompt)
        {
            var response = await client.PostAsJsonAsync(
                "/api/generate",
                new
                {
                    model = "deepseek-r1:8b",
                    prompt,
                    stream = false,
                    options = new { temperature = 0.2 }
                });

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
