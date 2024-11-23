using MediatR;
using PersonalFinanceApplication_DTO.NotificationModels;
using PersonalFinanceApplication_MBService.ProducerService;

namespace PersonalFinanceApplication_Services.NotificationHandlers.BalanceEventHandlers
{
    public class IncomeBalanceChangedEventHandler : INotificationHandler<IncomeBalanceEvent>
    {
        private readonly IProducerService _producerService;
        public IncomeBalanceChangedEventHandler(IProducerService producerService)
        {
            _producerService = producerService;
        }

        public Task Handle(IncomeBalanceEvent notification, CancellationToken cancellationToken)
        {
            _producerService.PublishMessageToUpdateBalanceQueue(notification);
            return Task.CompletedTask;
        }
    }
}
