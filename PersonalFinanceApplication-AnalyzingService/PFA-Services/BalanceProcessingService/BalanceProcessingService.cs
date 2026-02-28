using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;
using PFA_DAL.Abstraction;
using PFA_DM.Models;
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

        public void AccountBalanceOpeningService(BalanceChangedEvent response)
        {
            var accountBalance = response.UserContract.AccountBalance.ToModel();
            accountBalance.UserContractId = response.UserContract.UserContractId;
            _accountBalanceRepository.AddEntity(accountBalance);
        }

        public void SyncBalanceToAccount(BalanceChangedEvent response, TransactionType transactionType)
        {
            var accountBalance = _accountBalanceRepository.GetEntity(response.UserContract.UserContractId);
            accountBalance.Amount = ApplyBalanceChange(response, accountBalance, transactionType);
            _accountBalanceRepository.UpdateEntity(accountBalance, accountBalance);
        }

        private decimal ApplyBalanceChange(BalanceChangedEvent balanceChangedEvent, AccountBalance accountBalance, TransactionType transactionType)
        {
            if (transactionType is TransactionType.Income)
            {
                IncomeBalanceEvent incomeBalanceEvent = (IncomeBalanceEvent)balanceChangedEvent;
                accountBalance.LastDateAddedMoney = incomeBalanceEvent.Income.Date;
                return incomeBalanceEvent.FinalAmount;
            }
            else if (transactionType is TransactionType.Expense)
            {
                ExpenseBalanceEvent expenseBalanceEvent = (ExpenseBalanceEvent)balanceChangedEvent;
                accountBalance.LastDateDrawMoney = expenseBalanceEvent.Expense.Date;
                return expenseBalanceEvent.FinalAmount;
            }
            throw new CoreException("Invalid transaction type");
        }
    }
}
