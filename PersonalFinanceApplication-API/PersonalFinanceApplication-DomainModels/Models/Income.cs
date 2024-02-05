using PersonalFinanceApplication_DomainModels.Enums;

namespace PersonalFinanceApplication_DomainModels.Models
{
    public class Income
    {
        public int IncomeId { get; set; }
        public DateTime Date { get; set; }
        public Account Account { get; set; }
        public IncomeCategory Category { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Note { get; set; }
    }
}
