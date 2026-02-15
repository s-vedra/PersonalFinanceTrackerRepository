using MediatR;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DTO.DtoModels;

namespace PersonalFinanceApplication_DTO.NotificationModels
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
