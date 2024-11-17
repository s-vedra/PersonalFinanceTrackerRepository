using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApplication_DTO.RequestModels
{
    public class LatestCurrenciesDto
    {
        [Required]
        public string Base { get; set; }
        public string? Symbols { get; set; }
    }
}
