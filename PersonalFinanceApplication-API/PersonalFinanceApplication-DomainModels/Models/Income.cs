using PersonalFinanceApplication_DomainModels.Enums;

namespace PersonalFinanceApplication_DomainModels.Models
{
    public class Income
    {
        public Guid ReferenceId { get; set; } = Guid.NewGuid();
        public int IncomeId { get; set; }
        public DateTime Date { get; set; }
        public PaymentIssue PaymentIssue { get; set; }
        public IncomeCategory Category { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Note { get; set; }
        public int UserContractId { get; set; }
        public UserContract UserContract { get; set; }
    }
}
