using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceApplication_Services.EventServices.AiInsightEvent
{
    public interface IAiInsightEventService
    {
        Task EnsureAiInsightCachedAsync(UserContractSummaryDto userContractSummaryDto, SalarySchedulerDto salarySchedulerDto);
    }
}
