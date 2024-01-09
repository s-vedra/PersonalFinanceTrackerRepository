using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApplication_DtoModels.RequestModels
{
    public class HistoricalCurrenciesRequestDto
    {
        [Required]
        public string Base { get; set; }
        [Required]
        public string Date { get; set; }
        public string? Symbols { get; set; }

    }
}
