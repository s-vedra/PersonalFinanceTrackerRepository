using MassTransit;
using MediatR;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceApplication_Services.NotificationHandlers.BalanceEventHandlers
{
    public class IncomeBalanceChangedEventHandler : INotificationHandler<IncomeBalanceEvent>
    {
        private readonly IBus _bus;
        public IncomeBalanceChangedEventHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(IncomeBalanceEvent notification, CancellationToken cancellationToken)
        {
            await _bus.Publish(notification);
        }
    }
}
