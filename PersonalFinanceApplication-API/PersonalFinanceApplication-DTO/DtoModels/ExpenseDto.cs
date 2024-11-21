using PersonalFinanceApplication_DomainModels.Enums;

namespace PersonalFinanceApplication_DTO.DtoModels
{
    public class ExpenseDto
    {
        public int ExpenseId { get; set; }
        public DateTime Date { get; set; }
        public Account Account { get; set; }
        public ExpenseCategory Category { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Note { get; set; }
        public int UserContractId { get; set; }
    }
}
