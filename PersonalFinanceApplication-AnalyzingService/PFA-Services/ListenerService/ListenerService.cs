using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PFA_MBService.ConsumerService;
using PFA_Services.Abstractions;

namespace PFA_Services.AnalyzingService
{
    public class ListenerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ListenerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var consumerService = scope.ServiceProvider.GetRequiredService<IConsumerService>();
                var balanceProcessingService = scope.ServiceProvider.GetRequiredService<IBalanceProcessingService>();

                var response = consumerService.RecieveMessageFromAnalyzingQueue();
                balanceProcessingService.SyncBalanceToAccount(response);
            }
            return Task.CompletedTask;
        }
    }
}
