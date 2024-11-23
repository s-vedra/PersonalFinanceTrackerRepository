using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PFA_DTO.NotificationModels;
using PFA_DTO.ResponseModels;
using PFA_Exceptions.Exceptions;
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
                        var response = consumerService.RecieveMessageFromUpdateBalanceQueue();
                        if (!string.IsNullOrEmpty(response))
                            BalanceOperationProcess(response, balanceProcessingService);
                    }
                    catch (Exception ex)
                    {
                        throw new CoreException(ex, "Something went wrong when receiving the service");
                    }
                }
                await Task.Delay(5000, stoppingToken);
            }
        }

        private void BalanceOperationProcess(string response, IBalanceProcessingService balanceProcessingService)
        {
            var balanceOperationProcesser = JsonConvert.DeserializeObject<BalanceOperationProcessor>(response);
            if (balanceOperationProcesser.BalanceOperation is BalanceOperation.InitializeBalance)
            {
                balanceProcessingService.AccountBalanceOpeningService(response);
            }
            else if (balanceOperationProcesser.BalanceOperation is BalanceOperation.AdjustBalance)
            {
                balanceProcessingService.SyncBalanceToAccount(response);
            }
        }
    }
}
