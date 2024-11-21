using PFA_DTO.ResponseModels;

namespace PFA_DTO.NotificationModels
{
    public class BalanceChangedEvent
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public IncomeDto Income { get; set; }
        public ExpenseDto Expense { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public Account Account { get; set; }
        public UserContractDto UserContract { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
