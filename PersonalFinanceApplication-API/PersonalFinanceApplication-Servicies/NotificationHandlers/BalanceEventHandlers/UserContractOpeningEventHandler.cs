using MassTransit;
using MediatR;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceApplication_Services.NotificationHandlers.BalanceEventHandlers
{
    public class UserContractOpeningEventHandler : INotificationHandler<UserContractOpeningEvent>
    {
        private readonly IBus _bus;
        public UserContractOpeningEventHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(UserContractOpeningEvent notification, CancellationToken cancellationToken)
        {
            await _bus.Publish(notification);
        }
    }
}
