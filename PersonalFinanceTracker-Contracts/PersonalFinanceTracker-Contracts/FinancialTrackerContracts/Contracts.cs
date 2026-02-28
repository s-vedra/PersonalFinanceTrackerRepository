namespace PersonalFinanceTracker_Contracts.FinancialTrackerContracts
{
    public class IncomeDto
    {
        public Guid ReferenceId { get; set; }
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
        public Guid ReferenceId { get; set; }
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
        public AccountBalanceDto AccountBalance { get; set; }
        public string ContractName { get; set; }
        public ContractType ContractType { get; set; }
        public IEnumerable<IncomeDto> Incomes { get; set; }
        public IEnumerable<ExpenseDto> Expenses { get; set; }
        public int UserId { get; set; }
        public UserContractStatus UserContractStatus { get; set; }
        public DateTime DateOpened { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }

    public class UserContractSummaryDto
    {
        public int UserContractId { get; set; }
        public AccountBalanceDto AccountBalance { get; set; }
        public string ContractName { get; set; }
        public ContractType ContractType { get; set; }
        public IEnumerable<IncomeDto> Incomes { get; set; }
        public IEnumerable<ExpenseDto> Expenses { get; set; }
        public int UserId { get; set; }
        public UserContractStatus UserContractStatus { get; set; }
        public DateTime DateOpened { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public string AIInsight { get; set; }
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

    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public IEnumerable<UserContractDto> UserContracts { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }
}
