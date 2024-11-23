using MediatR;
using PersonalFinanceApplication_DTO.NotificationModels;
using PersonalFinanceApplication_MBService.ProducerService;

namespace PersonalFinanceApplication_Services.NotificationHandlers.BalanceEventHandlers
{
    public class UserContractOpeningEventHandler : INotificationHandler<UserContractOpeningEvent>
    {
        private readonly IProducerService _producerService;
        public UserContractOpeningEventHandler(IProducerService producerService)
        {
            _producerService = producerService;
        }

        public Task Handle(UserContractOpeningEvent notification, CancellationToken cancellationToken)
        {
            _producerService.PublishMessageToUpdateBalanceQueue(notification);
            return Task.CompletedTask;
        }
    }
}
