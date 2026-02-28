using MassTransit;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;
using PFA_Services.Abstractions;

namespace PFA_Services.ConsumerService
{
    public class ExpenseBalanceConsumer : IConsumer<ExpenseBalanceEvent>
    {
        private readonly IBalanceProcessingService _balanceProcessingService;
        public ExpenseBalanceConsumer(IBalanceProcessingService balanceProcessingService)
        {
            _balanceProcessingService = balanceProcessingService;
        }

        public async Task Consume(ConsumeContext<ExpenseBalanceEvent> context)
        {
            _balanceProcessingService.SyncBalanceToAccount(context.Message, context.Message.TransactionType);
        }
    }
}

