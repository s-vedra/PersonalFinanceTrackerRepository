using MediatR;
using PersonalFinanceApplication_DTO.NotificationModels;
using PersonalFinanceApplication_MBService.ProducerService;

namespace PersonalFinanceApplication_Services.NotificationHandlers.BalanceEventHandlers
{
    public class ExpenseBalanceChangedEventHandler : INotificationHandler<ExpenseBalanceEvent>
    {
        private readonly IProducerService _producerService;
        public ExpenseBalanceChangedEventHandler(IProducerService producerService)
        {
            _producerService = producerService;
        }

        public Task Handle(ExpenseBalanceEvent notification, CancellationToken cancellationToken)
        {
            _producerService.PublishMessageToUpdateBalanceQueue(notification);
            return Task.CompletedTask;
        }
    }
}
