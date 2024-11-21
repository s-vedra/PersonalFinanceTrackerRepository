using MediatR;
using PersonalFinanceApplication_DTO.NotificationModels;
using PersonalFinanceApplication_MBService.ProducerService;

namespace PersonalFinanceApplication_Services.NotificationHandler
{
    public class BalanceChangeEventHandler : INotificationHandler<BalanceChangedEvent>
    {
        private readonly IProducerService _producerService;
        public BalanceChangeEventHandler(IProducerService producerService)
        {
            _producerService = producerService;
        }

        public Task Handle(BalanceChangedEvent notification, CancellationToken cancellationToken)
        {
            _producerService.PublishMessageToAnalyzingQueue(notification);
            return Task.CompletedTask;
        }
    }
}
