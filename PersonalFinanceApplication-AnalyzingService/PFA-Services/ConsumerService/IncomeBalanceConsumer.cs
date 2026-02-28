using MassTransit;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;
using PFA_Services.Abstractions;

namespace PFA_Services.ConsumerService
{
    public class IncomeBalanceConsumer : IConsumer<IncomeBalanceEvent>
    {
        private readonly IBalanceProcessingService _balanceProcessingService;
        public IncomeBalanceConsumer(IBalanceProcessingService balanceProcessingService)
        {
            _balanceProcessingService = balanceProcessingService;
        }

        public async Task Consume(ConsumeContext<IncomeBalanceEvent> context)
        {
            _balanceProcessingService.SyncBalanceToAccount(context.Message, context.Message.TransactionType);
        }
    }
}
