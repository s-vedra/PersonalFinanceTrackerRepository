using gRPCClient;
using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;

namespace PersonalFinanceApplication_Mappers.Mappers
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
                IsActive = accountBalance.IsActive,
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
                IsActive = accountBalance.IsActive,
                Created = accountBalance.Created
            };
        }

        public static AccountBalanceDto MapAcccountBalanceRequest(this AccountBalanceResponse accountBalanceRequest)
        {
            return new AccountBalanceDto()
            {
                AccountBalanceId = accountBalanceRequest.AccountBalanceId,
                Amount = accountBalanceRequest.Amount,
                Currency = accountBalanceRequest.Currency,
                LastDateAddedMoney = accountBalanceRequest.LastDateAddedMoney.ToDateTime(),
                LastDateDrawMoney = accountBalanceRequest.LastDateDrawMoney.ToDateTime(), 
                UserContractId = accountBalanceRequest.UserContractId,
                IsActive = accountBalanceRequest.IsActive,
                Created = accountBalanceRequest.Created.ToDateTime()
            };
        }
    }
}
