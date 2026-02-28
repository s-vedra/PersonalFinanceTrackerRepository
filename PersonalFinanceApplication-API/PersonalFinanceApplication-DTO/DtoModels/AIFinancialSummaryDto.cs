namespace PersonalFinanceApplication_DTO.DtoModels
{
    public class AIFinancialSummaryDto
    {
        public string Period { get; set; }
        public int TotalIncome { get; set; }
        public int TotalExpenses { get; set; }
        public int NetSavings { get; set; }
        public int SavingsRate { get; set; }
        public int TopExpenseCategories { get; set; }
        public int MonthlyTrend { get; set; }
        public int AverageExpenseAmount { get; set; }
        public int ExpenseVolatility { get; set; }
    }
}
