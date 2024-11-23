using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DTO.DtoModels;

namespace PersonalFinanceApplication_Services.EventServices.BalanceEvent
{
    public interface IBalanceEventService
    {
        Task InitializeBalanceOnContractCreation(UserContractDto userContract, AccountBalanceDto accountBalance,
            BalanceOperation balanceOperation);
        Task AdjustBalanceOnRecievedIncome(UserContractDto userContract, IncomeDto income,
            TransactionType transactionType, BalanceOperation balanceOperation);
        Task AdjustBalanceOnRecievedExpense(UserContractDto userContract, ExpenseDto expense,
           TransactionType transactionType, BalanceOperation balanceOperation);
    }
}
