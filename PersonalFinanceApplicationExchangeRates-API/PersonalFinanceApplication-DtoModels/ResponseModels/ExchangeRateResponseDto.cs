namespace PersonalFinanceApplicationTransfer_API.Models
{
    public class ExchangeRateResponseDto
    {
        public DateTime Date { get; set; }
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
