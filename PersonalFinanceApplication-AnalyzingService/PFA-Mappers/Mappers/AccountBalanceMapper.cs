using PFA_DM.Models;
using PFA_DTO.ResponseModels;

namespace PFA_Mappers.Mappers
{
    public static class AccountBalanceMapper
    {
        public static AccountBalanceDto ToDto(this AccountBalance accountBalance)
        {
            return new AccountBalanceDto
            {
                AccountBalanceId = accountBalance.AccountBalanceId,
                Amount = accountBalance.Amount,
                Currency = accountBalance.Currency,
                LastDateAddedMoney = accountBalance.LastDateAddedMoney,
                LastDateDrawMoney = accountBalance.LastDateDrawMoney
            };
        }

        public static AccountBalance ToModel(this AccountBalanceDto accountBalance)
        {
            return new AccountBalance
            {
                AccountBalanceId = accountBalance.AccountBalanceId,
                Amount = accountBalance.Amount,
                Currency = accountBalance.Currency,
                LastDateAddedMoney = accountBalance.LastDateAddedMoney,
                LastDateDrawMoney = accountBalance.LastDateDrawMoney
            };
        }
    }
}
