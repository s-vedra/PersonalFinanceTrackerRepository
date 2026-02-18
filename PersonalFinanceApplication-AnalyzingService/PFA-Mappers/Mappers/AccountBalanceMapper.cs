using Google.Protobuf.WellKnownTypes;
using gRPCClient;
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
                LastDateDrawMoney = accountBalance.LastDateDrawMoney, 
                UserContractId = accountBalance.UserContractId,
                Created = accountBalance.Created
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
                LastDateDrawMoney = accountBalance.LastDateDrawMoney,
                UserContractId = accountBalance.UserContractId,
                Created = accountBalance.Created
            };
        }

        public static AccountBalanceResponse MapAccountBalanceRequest(this AccountBalance accountBalance)
        {
            return new AccountBalanceResponse
            {
                AccountBalanceId = accountBalance.AccountBalanceId,
                Amount = (long)accountBalance.Amount,
                Currency = accountBalance.Currency,
                LastDateAddedMoney = accountBalance.LastDateAddedMoney.ToUniversalTime().ToTimestamp(),
                LastDateDrawMoney = accountBalance.LastDateDrawMoney.ToUniversalTime().ToTimestamp(),
                UserContractId = accountBalance.UserContractId,
                IsActive = accountBalance.IsActive,
                Created = accountBalance.Created.ToUniversalTime().ToTimestamp()
            };
        }
    }
}
