using PersonalFinanceTracker_Contracts.AiInsightsContracts;
using PFA_DTO.FinancialSnapshotPromptCalculationModels;

namespace PFA_Services.FinancialSummaryCalculationService
{
    public interface IFinancialSummaryCalculationService
    {
        FinancialSnapshotPromptCalculationModel CalculateFinancialSnapshotPrompt(FinancialSnapshot financialSnapshot);
    }
}
