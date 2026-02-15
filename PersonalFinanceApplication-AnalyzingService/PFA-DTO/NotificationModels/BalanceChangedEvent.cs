using PFA_DTO.ResponseModels;

namespace PFA_DTO.NotificationModels
{
    public abstract class BalanceChangedEvent
    {
        public int UserId { get; set; }
        public UserContractDto UserContract { get; set; }
        public BalanceOperation BalanceOperation { get; set; }
    }

    public class UserContractOpeningEvent : BalanceChangedEvent
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class IncomeBalanceEvent : BalanceChangedEvent
    {
        public IncomeDto Income { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        public PaymentIssue PaymentIssue { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal FinalAmount { get; set; }

    }

    public class ExpenseBalanceEvent : BalanceChangedEvent
    {
        public ExpenseDto Expense { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public PaymentIssue PaymentIssue { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal FinalAmount { get; set; }
    }

    public class BalanceChangedEventWrapper
    {
        public TransactionType TransactionType { get; set; }
        public UserContractDto UserContract { get; set; }
    }

    public class BalanceOperationProcessor
    {
        public BalanceOperation BalanceOperation { get; set; }
    }
}
