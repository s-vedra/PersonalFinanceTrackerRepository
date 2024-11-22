namespace PersonalFinanceApplication_MBService.ProducerService
{
    public interface IProducerService
    {
        void PublishMessageToUpdateBalanceQueue<T>(T request);
    }
}
