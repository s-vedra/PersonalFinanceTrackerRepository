namespace PersonalFinanceApplication_DTO.ResponseModels
{
    public class ExchangeRateResponseDto
    {
        public MetaDto Meta { get; set; }
        public ResponseDto Response { get; set; }
        public DateTime Date { get; set; }
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }

    public class MetaDto
    {
        public int Code { get; set; }
        public string Disclaimer { get; set; }
    }

    public class ResponseDto
    {
        public DateTime Date { get; set; }
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
