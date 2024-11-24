using Newtonsoft.Json;
using PFA_DAL.Abstraction;
using PFA_DM.Models;
using PFA_DTO.NotificationModels;
using PFA_DTO.ResponseModels;
using PFA_Exceptions.Exceptions;
using PFA_Mappers.Mappers;
using PFA_Services.Abstractions;

namespace PFA_Services.BalanceProcessingService
{
    public class BalanceProcessingService : IBalanceProcessingService
    {
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        public BalanceProcessingService(IAccountBalanceRepository accountBalanceRepository)
        {
            _accountBalanceRepository = accountBalanceRepository;
        }

        public void AccountBalanceOpeningService(string response)
        {
            var balanceOperationData = JsonConvert.DeserializeObject<BalanceOperationData>(response);
            var accountBalance = balanceOperationData.UserContract.AccountBalance.ToModel();
            accountBalance.UserContractId = balanceOperationData.UserContract.UserContractId;
            _accountBalanceRepository.AddEntity(accountBalance);
        }

        public void SyncBalanceToAccount(string response)
        {
            if (!string.IsNullOrEmpty(response))
            {
                var balanceChangedEventWrapper = JsonConvert.DeserializeObject<BalanceChangedEventWrapper>(response);
                var accountBalance = _accountBalanceRepository.GetEntity(balanceChangedEventWrapper.UserContract.UserContractId);

                accountBalance.Amount = CalculateAccountBalance(balanceChangedEventWrapper, accountBalance, response);
                _accountBalanceRepository.UpdateEntity(accountBalance, accountBalance);
            }
        }

        private decimal CalculateAccountBalance(BalanceChangedEventWrapper balanceChangedEvent,
            AccountBalance accountBalance, string response)
        {
            if (balanceChangedEvent.TransactionType is TransactionType.Income)
            {
                var incomeBalanceEvent = JsonConvert.DeserializeObject<IncomeBalanceEvent>(response);
                accountBalance.LastDateAddedMoney = incomeBalanceEvent.Income.Date;
                return accountBalance.Amount += incomeBalanceEvent.Income.Amount;
            }
            else if (balanceChangedEvent.TransactionType == TransactionType.Expense)
            {
                var expenseBalanceEvent = JsonConvert.DeserializeObject<ExpenseBalanceEvent>(response);
                if (accountBalance.Amount < expenseBalanceEvent.Expense.Amount)
                    throw new CoreException("The account balance is insufficient for the expense amount.");
                accountBalance.LastDateDrawMoney = expenseBalanceEvent.Expense.Date;
                return accountBalance.Amount -= expenseBalanceEvent.Expense.Amount;
            }
            throw new CoreException("Invalid transaction type");
        }
    }
}
