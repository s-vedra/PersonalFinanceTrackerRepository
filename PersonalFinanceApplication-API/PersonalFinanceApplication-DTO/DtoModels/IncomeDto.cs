using PersonalFinanceApplication_DomainModels.Enums;

namespace PersonalFinanceApplication_DTO.DtoModels
{
    public class IncomeDto
    {
        public int IncomeId { get; set; }
        public DateTime Date { get; set; }
        public PaymentIssue PaymentIssue { get; set; }
        public IncomeCategory Category { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Note { get; set; }
        public int UserContractId { get; set; }
    }
}
