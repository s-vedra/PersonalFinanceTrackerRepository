namespace PersonalFinanceApplicationExchangeRates_API.Models
{
    public class CurrencyConversionResponseDto
    {
        public string Date { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public double Amount { get; set; }

        public double Value { get; set; }
    }
}
