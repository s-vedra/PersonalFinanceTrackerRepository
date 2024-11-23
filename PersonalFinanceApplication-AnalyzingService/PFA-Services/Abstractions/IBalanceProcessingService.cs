namespace PFA_Services.Abstractions
{
    public interface IBalanceProcessingService
    {
        void SyncBalanceToAccount(string response);
        void AccountBalanceOpeningService(string response);
    }
}
