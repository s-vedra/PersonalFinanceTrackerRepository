using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceTracker_Contracts.AiInsightsContracts
{
    public class FinancialSnapshot
    {
        public int UserContractId { get; set; }
        public int UserId { get; set; }
        public string Period { get; set; }
        public decimal TotalSalary { get; set; }
        public IEnumerable<ExpenseDto> Expenses { get; set; }
        public IEnumerable<IncomeDto> Incomes { get; set; }
        public decimal TotalAmountOnAccount { get; set; }
        public string Currency { get; set; }
    }
}
