using Microsoft.Extensions.Hosting;
using PFA_MBService.ConsumerService;

namespace PFA_Services.AnalyzingService
{
    public class ListenerService : BackgroundService
    {
        private readonly IConsumerService _consumerService;

        public ListenerService(IConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumerService.RecieveMessageFromAnalyzingQueue();
            return Task.CompletedTask;
        }
    }
}
