using PFA_DTO.RequestModels;
using PFA_DTO.ResponseModels;
using Refit;

namespace PFA_Services.AIInsightService
{
    public interface IAiClient
    {
        [Post("/api/generate")]
        Task<AiInsightResponseModel> GenerateAsync([Body] AiInsightRequestModel request);
    }
}
