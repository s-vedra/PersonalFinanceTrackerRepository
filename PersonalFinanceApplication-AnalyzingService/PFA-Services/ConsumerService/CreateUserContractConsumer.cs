using MassTransit;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;
using PFA_Services.Abstractions;

namespace PFA_Services.ConsumerService
{
    public class CreateUserContractConsumer : IConsumer<UserContractOpeningEvent>
    {
        private readonly IBalanceProcessingService _balanceProcessingService;
        public CreateUserContractConsumer(IBalanceProcessingService balanceProcessingService)
        {
            _balanceProcessingService = balanceProcessingService;
        }

        public async Task Consume(ConsumeContext<UserContractOpeningEvent> context)
        {
            _balanceProcessingService.AccountBalanceOpeningService(context.Message);
        }
    }
}
