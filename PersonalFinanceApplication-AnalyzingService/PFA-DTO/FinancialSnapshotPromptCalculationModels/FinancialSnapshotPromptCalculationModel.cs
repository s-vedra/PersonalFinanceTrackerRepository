using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PFA_DTO.FinancialSnapshotPromptCalculationModels
{
    public class FinancialSnapshotPromptCalculationModel
    {
        public string Period { get; set; }
        public decimal TotalIncomeAmount { get; set; }
        public decimal TotalExpenseAmount { get; set; }
        public decimal NetSavings { get; set; }
        public decimal SavingsRate { get; set; }
        public decimal AverageExpenseAmount { get; set; }
        public decimal ExpenseVolatility { get; set; }
        public IEnumerable<TopCategories> TopCategories { get; set; }
        public MonthlyTrend MonthlyTrend { get; set; }
        public string Currency { get; set; }
    }

    public class TopCategories
    {
        public string ExpenseCategory { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
    }

    public class MonthlyTrend
    {
        public string Month { get; set; }
        public decimal IncomeAmount { get; set; }
        public decimal ExpenseAmount { get; set; }
    }
}
