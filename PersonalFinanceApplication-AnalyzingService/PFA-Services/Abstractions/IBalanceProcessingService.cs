using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PFA_Services.Abstractions
{
    public interface IBalanceProcessingService
    {
        void SyncBalanceToAccount(BalanceChangedEvent response, TransactionType transactionType);
        void AccountBalanceOpeningService(BalanceChangedEvent response);
    }
}
