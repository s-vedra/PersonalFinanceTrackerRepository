namespace PFA_DTO.RequestModels
{
    public class AiInsightRequestModel
    {
        public string Model { get; set; }
        public string Prompt { get; set; }
        public bool Stream { get; set; }
        public AiOptions Options { get; set; }
    }

    public class AiOptions
    {
        public double Temperature { get; set; }
    }
}