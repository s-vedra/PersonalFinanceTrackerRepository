using Newtonsoft.Json;
using PFA_DAL.Abstraction;
using PFA_DM.Models;
using PFA_DTO.NotificationModels;
using PFA_DTO.ResponseModels;
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
        public void SyncBalanceToAccount(string response)
        {
            var balanceChangedEvent = JsonConvert.DeserializeObject<BalanceChangedEvent>(response);
            if (balanceChangedEvent is not null)
            {
                var accountBalance = _accountBalanceRepository.GetEntity(balanceChangedEvent.UserContract.UserContractId);
                accountBalance.Amount = CalculateAccountBalance(balanceChangedEvent, accountBalance);
                _accountBalanceRepository.UpdateEntity(accountBalance, accountBalance);
            }
        }

        private decimal CalculateAccountBalance(BalanceChangedEvent balanceChangedEvent, AccountBalance accountBalance)
        {
            if (balanceChangedEvent.TransactionType == TransactionType.Income)
            {
                accountBalance.LastDateAddedMoney = balanceChangedEvent.Date;
                return accountBalance.Amount += balanceChangedEvent.Income.Amount;
            }
            else if (balanceChangedEvent.TransactionType == TransactionType.Expense)
            {
                accountBalance.LastDateDrawMoney = balanceChangedEvent.Date;
                return accountBalance.Amount -= balanceChangedEvent.Expense.Amount;
            }

            throw new Exception("Invalid transaction type");
        }
    }
}
