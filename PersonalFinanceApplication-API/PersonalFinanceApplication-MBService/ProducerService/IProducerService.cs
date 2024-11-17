namespace PersonalFinanceApplication_MBService.ProducerService
{
    public interface IProducerService
    {
        void PublishMessageToAnalyzingQueue(string request);
    }
}
