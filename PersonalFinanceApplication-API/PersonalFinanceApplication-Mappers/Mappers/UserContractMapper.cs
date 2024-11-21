using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;

namespace PersonalFinanceApplication_Mappers.Mappers
{
    public static class UserContractMapper
    {
        public static UserContractDto ToDto(this UserContract userContract)
        {
            return new UserContractDto
            {
                UserContractId = userContract.UserContractId, 
                AccountBalanceId = userContract.AccountBalanceId, 
                ContractType = userContract.ContractType, 
                UserId = userContract.UserId
            };
        }

        public static UserContract ToModel(this UserContractDto userContract)
        {
            return new UserContract
            {
                UserContractId = userContract.UserContractId,
                AccountBalanceId = userContract.AccountBalanceId,
                ContractType = userContract.ContractType,
                UserId = userContract.UserId
            };
        }
    }
}
