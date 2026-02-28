using MassTransit;
using MediatR;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceApplication_Services.NotificationHandlers.BalanceEventHandlers
{
    public class ExpenseBalanceChangedEventHandler : INotificationHandler<ExpenseBalanceEvent>
    {
        private readonly IBus _bus;
        public ExpenseBalanceChangedEventHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(ExpenseBalanceEvent notification, CancellationToken cancellationToken)
        {
            await _bus.Publish(notification);
        }
    }
}
