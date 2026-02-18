namespace PFA_DTO.ResponseModels
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
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }

    public class ExpenseDto
    {
        public int ExpenseId { get; set; }
        public DateTime Date { get; set; }
        public PaymentIssue PaymentIssue { get; set; }
        public ExpenseCategory Category { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Note { get; set; }
        public int UserContractId { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }

    public class UserContractDto
    {
        public int UserContractId { get; set; }
        public int AccountBalanceId { get; set; }
        public AccountBalanceDto AccountBalance { get; set; }
        public ContractType ContractType { get; set; }
        public ICollection<IncomeDto> Incomes { get; set; }
        public ICollection<ExpenseDto> Expenses { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }

    public class AccountBalanceDto
    {
        public int AccountBalanceId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime LastDateAddedMoney { get; set; }
        public DateTime LastDateDrawMoney { get; set; }
        public int UserContractId { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }

    public class BalanceOperationData
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public UserContractDto UserContract { get; set; }
        public int BalanceOperation { get; set; }
    }
}
