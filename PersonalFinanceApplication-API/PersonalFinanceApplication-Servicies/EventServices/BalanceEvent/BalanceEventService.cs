using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_DTO.NotificationModels;
using PersonalFinanceApplication_Exceptions.Exceptions;

namespace PersonalFinanceApplication_Services.EventServices.BalanceEvent
{
    public class BalanceEventService : IBalanceEventService
    {
        private readonly IMediator _mediator;
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        public BalanceEventService(IMediator mediator, IAccountBalanceRepository accountBalanceRepository)
        {
            _mediator = mediator;
            _accountBalanceRepository = accountBalanceRepository;
        }

        public async Task InitializeBalanceOnContractCreation(UserContractDto userContract, AccountBalanceDto accountBalanceDto,
            BalanceOperation balanceOperation)
        {
            var notification = UserContractOpeningNotification(userContract, accountBalanceDto);
            await PublishNotificationToProducer(notification, balanceOperation);
        }

        public async Task AdjustBalanceOnRecievedIncome(UserContractDto userContract, IncomeDto income,
            TransactionType transactionType, BalanceOperation balanceOperation)
        {
            var notification = IncomeBalanceChangeNotification(userContract, income);
            notification.TransactionType = transactionType;
            var finalAmount = AdjustAccountBalanceOnUserContract(userContract, notification, transactionType);
            notification.FinalAmount = finalAmount;
            await PublishNotificationToProducer(notification, balanceOperation);
        }

        public async Task AdjustBalanceOnRecievedExpense(UserContractDto userContract, ExpenseDto expense,
           TransactionType transactionType, BalanceOperation balanceOperation)
        {
            var notification = ExpenseBalanceChangeNotification(userContract, expense);
            notification.TransactionType = transactionType;
            var finalAmount = AdjustAccountBalanceOnUserContract(userContract, notification, transactionType);
            notification.FinalAmount = finalAmount;
            await PublishNotificationToProducer(notification, balanceOperation);
        }

        private async Task PublishNotificationToProducer(BalanceChangedEvent notification, BalanceOperation balanceOperation)
        {
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

        private decimal AdjustAccountBalanceOnUserContract(UserContractDto userContract, BalanceChangedEvent notification, TransactionType transactionType)
        {
            var accountBalance = RetrieveAccountBalance(userContract);
            var amount = CalculateAccountBalanceOnUserContract(userContract, notification, transactionType, accountBalance);
            _accountBalanceRepository.UpdateEntity(accountBalance, accountBalance);
            return amount;
        }

        private AccountBalance RetrieveAccountBalance(UserContractDto userContractDto)
        {
            return _accountBalanceRepository.GetEntity(userContractDto.UserContractId);
        }

        private decimal CalculateAccountBalanceOnUserContract(UserContractDto userContract, BalanceChangedEvent balanceChangedEvent, TransactionType transactionType,
            AccountBalance accountBalance)
        {
            if (balanceChangedEvent is null) throw new CoreException("Balance event cannot be null!");

            if (transactionType is TransactionType.Income)
            {
                var incomeEvent = balanceChangedEvent as IncomeBalanceEvent;
                accountBalance.LastDateAddedMoney = incomeEvent.Income.Date;
                return accountBalance.Amount += incomeEvent.Income.Amount;
            }
            else if (transactionType is TransactionType.Expense)
            {
                var expenseEvent = balanceChangedEvent as ExpenseBalanceEvent;
                if (accountBalance.Amount < expenseEvent.Expense.Amount)
                    throw new CoreException("The account balance is insufficient for the expense amount.");
                accountBalance.LastDateDrawMoney = expenseEvent.Expense.Date;
                return accountBalance.Amount -= expenseEvent.Expense.Amount;
            }
            throw new CoreException("Invalid transaction type");
        }
    }
}
