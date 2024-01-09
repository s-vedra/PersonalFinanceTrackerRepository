using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApplicationExchangeRates_API.Models
{
    public class ExchangeAmountRequestDto
    {
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string Amount { get; set; }
    }
}
