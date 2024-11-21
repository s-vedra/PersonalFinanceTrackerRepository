using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PFA_MBService.ConsumerService;
using PFA_Services.Abstractions;

namespace PFA_Services.AnalyzingService
{
    public class ListenerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ListenerService> _logger;

        public ListenerService(IServiceProvider serviceProvider, ILogger<ListenerService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var consumerService = scope.ServiceProvider.GetRequiredService<IConsumerService>();
                    var balanceProcessingService = scope.ServiceProvider.GetRequiredService<IBalanceProcessingService>();

                    try
                    {
                        var response = consumerService.RecieveMessageFromAnalyzingQueue();
                        if (!string.IsNullOrEmpty(response))
                        {
                            balanceProcessingService.SyncBalanceToAccount(response);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing message from queue.");
                    }
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
