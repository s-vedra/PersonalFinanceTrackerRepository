using System.Text.Json.Serialization;

namespace PFA_DTO.ResponseModels
{
    public class AiInsightResponseModel
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("response")]
        public string Response { get; set; }
    }
}
