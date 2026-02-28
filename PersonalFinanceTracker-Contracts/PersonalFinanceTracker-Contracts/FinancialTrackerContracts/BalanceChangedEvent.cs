using MediatR;

namespace PersonalFinanceTracker_Contracts.FinancialTrackerContracts
{
    public abstract class BalanceChangedEvent
    {
        public int UserId { get; set; }
        public UserContractDto UserContract { get; set; }
        public BalanceOperation BalanceOperation { get; set; }
    }

    public class UserContractOpeningEvent : BalanceChangedEvent, INotification
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class IncomeBalanceEvent : BalanceChangedEvent, INotification
    {
        public IncomeDto Income { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        public PaymentIssue PaymentIssue { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal FinalAmount { get; set; }

    }

    public class ExpenseBalanceEvent : BalanceChangedEvent, INotification
    {
        public ExpenseDto Expense { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public PaymentIssue PaymentIssue { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal FinalAmount { get; set; }
    }
}
