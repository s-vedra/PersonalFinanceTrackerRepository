using MediatR;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_DTO.NotificationModels;

namespace PersonalFinanceApplication_Services.EventServices.BalanceEvent
{
    public class BalanceEventService : IBalanceEventService
    {
        private readonly IMediator _mediator;
        public BalanceEventService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task InitializeBalanceOnContractCreation(UserContractDto userContract, AccountBalanceDto accountBalanceDto,
            BalanceOperation balanceOperation)
        {
            var notification = UserContractOpeningNotification(userContract, accountBalanceDto);
            notification.BalanceOperation = balanceOperation;

            await _mediator.Publish(notification);
        }

        public async Task AdjustBalanceOnRecievedIncome(UserContractDto userContract, IncomeDto income,
            TransactionType transactionType, BalanceOperation balanceOperation)
        {
            var notification = IncomeBalanceChangeNotification(userContract, income);
            notification.TransactionType = transactionType;
            notification.BalanceOperation = balanceOperation;

            await _mediator.Publish(notification);
        }

        public async Task AdjustBalanceOnRecievedExpense(UserContractDto userContract, ExpenseDto expense,
           TransactionType transactionType, BalanceOperation balanceOperation)
        {
            var notification = ExpenseBalanceChangeNotification(userContract, expense);
            notification.TransactionType = transactionType;
            notification.BalanceOperation = balanceOperation;

            await _mediator.Publish(notification);
        }

        private UserContractOpeningEvent UserContractOpeningNotification(UserContractDto userContract,
            AccountBalanceDto accountBalanceDto)
        {
            return new UserContractOpeningEvent()
            {
                UserId = userContract.UserId,
                Amount = accountBalanceDto.Amount,
                Date = accountBalanceDto.LastDateAddedMoney,
                UserContract = userContract
            };
        }

        private IncomeBalanceEvent IncomeBalanceChangeNotification(UserContractDto userContract, IncomeDto income)
        {
            return new IncomeBalanceEvent()
            {
                Income = income,
                IncomeCategory = income.Category,
                PaymentIssue = income.PaymentIssue,
                UserContract = userContract,
                UserId = userContract.UserId
            };
        }

        private ExpenseBalanceEvent ExpenseBalanceChangeNotification(UserContractDto userContract, ExpenseDto expense)
        {
            return new ExpenseBalanceEvent()
            {
                Expense = expense,
                ExpenseCategory = expense.Category,
                PaymentIssue = expense.PaymentIssue,
                UserContract = userContract,
                UserId = userContract.UserId
            };
        }
    }
}
