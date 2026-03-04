using PersonalFinanceTracker_Contracts.AiInsightsContracts;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;
using PFA_DTO.FinancialSnapshotPromptCalculationModels;
using PFA_Services.HelperMethods;

namespace PFA_Services.FinancialSummaryCalculationService
{
    public class FinancialSummaryCalculationService : IFinancialSummaryCalculationService
    {
        public FinancialSnapshotPromptCalculationModel CalculateFinancialSnapshotPrompt(FinancialSnapshot financialSnapshot)
        {
            var financialSnapshotPromptCalculationModel = new FinancialSnapshotPromptCalculationModel()
            {
                Period = financialSnapshot.Period,
                TotalExpenseAmount = CalculateTotalAmount(financialSnapshot.Expenses, x => x.Amount),
                TotalIncomeAmount = CalculateTotalAmount(financialSnapshot.Incomes, x => x.Amount),
                AverageExpenseAmount = CalculateAverageAmount(financialSnapshot.Expenses, x => x.Amount),
                ExpenseVolatility = CalculateExpenseVolatility(financialSnapshot),
                NetSavings = CalculateNetSavings(financialSnapshot),
                SavingsRate = CalculateSavingsRate(financialSnapshot),
                TopCategories = GetTopSpendingCategories(financialSnapshot.Expenses),
                MonthlyTrend = CalculateMonthlyTrend(financialSnapshot),
                Currency = financialSnapshot.Currency
            };

            return financialSnapshotPromptCalculationModel;
        }

        private decimal CalculateExpenseVolatility(FinancialSnapshot financialSnapshot)
        {
            var expenses = financialSnapshot.Expenses.Select(e => e.Amount).ToList();

            if (!expenses.Any()) return 0;

            var average = CalculateAverageAmount(expenses, x => x);

            var variance = expenses
                .Select(x => Math.Pow((double)(x - average), 2))
                .Average();

            var volatility = (decimal)Math.Sqrt(variance);

            return average == 0 ? 0 : Math.Round(volatility / average, 2);
        }

        private decimal CalculateNetSavings(FinancialSnapshot financialSnapshot)
        {
            var totalExpenses = CalculateTotalAmount(financialSnapshot.Expenses, x => x.Amount);
            var totalIncome = CalculateTotalAmount(financialSnapshot.Incomes, x => x.Amount);

            return Math.Round(totalIncome - totalExpenses, 2);
        }

        private decimal CalculateSavingsRate(FinancialSnapshot financialSnapshot)
        {
            var totalIncome = CalculateTotalAmount(financialSnapshot.Incomes, x => x.Amount);
            var netSavings = CalculateNetSavings(financialSnapshot);

            if (totalIncome == 0) return 0;
            return Math.Round((netSavings / totalIncome) * 100, 2);
        }

        private IEnumerable<TopCategories> GetTopSpendingCategories(IEnumerable<ExpenseDto> expenses)
        {
            var totalExpenses = CalculateTotalAmount(expenses, x => x.Amount);

            var topCategories = expenses.GroupBy(e => e.Category)
                .Select(g => new TopCategories
                {
                    ExpenseCategory = g.Key.ToString(),
                    Amount = CalculateTotalAmount(g, x => x.Amount),
                    Percentage = totalExpenses == 0 ? 0 : Math.Round((CalculateTotalAmount(g, x => x.Amount) / totalExpenses) * 100, 2)
                })
                .OrderByDescending(x => x.Amount)
                .Take(3)
                .ToList();

            return topCategories;
        }

        private MonthlyTrend CalculateMonthlyTrend(FinancialSnapshot financialSnapshot)
        {
            var monthlyTrend = new MonthlyTrend()
            {
                Month = DateTime.Now.ToString("MMMM"),
                ExpenseAmount = CalculateTotalAmount(financialSnapshot.Expenses, x => x.Amount),
                IncomeAmount = CalculateTotalAmount(financialSnapshot.Incomes, x => x.Amount)
            };

            return monthlyTrend;
        }

        private decimal CalculateTotalAmount<T>(IEnumerable<T> entities, Func<T, decimal> selector)
        {
            return Math.Round(entities.Sum(selector), 2);
        }

        private decimal CalculateAverageAmount<T>(IEnumerable<T> entities, Func<T, decimal> selector)
        {
            if (!entities.IsNull() && entities.Any())
                return Math.Round(entities.Average(selector), 2);
            return 0;
        }
    }
}
