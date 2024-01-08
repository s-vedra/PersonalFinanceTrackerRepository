namespace PersonalFinanceApplication_DTO.ResponseModels
{
    public class CurrencyConversionResponseDto
    {
        public Meta Meta { get; set; }

        public Response Response { get; set; }

        public int Timestamp { get; set; }

        public string Date { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public double Amount { get; set; }

        public double Value { get; set; }
    }

    public class Response
    {
        public int Timestamp { get; set; }

        public string Date { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public double Amount { get; set; }

        public double Value { get; set; }
    }
}
