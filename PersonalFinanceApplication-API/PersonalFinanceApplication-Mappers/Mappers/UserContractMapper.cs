using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceApplication_Mappers.Mappers
{
    public static class UserContractMapper
    {
        public static UserContractDto ToDto(this UserContract userContract)
        {
            return new UserContractDto
            {
                UserContractId = userContract.UserContractId,
                ContractType = (ContractType)userContract.ContractType,
                UserId = userContract.UserId,
                ContractName = userContract.ContractName,
                DateOpened = userContract.DateOpened,
                UserContractStatus = (UserContractStatus)userContract.UserContractStatus,
                IsActive = userContract.IsActive,
                Created = userContract.Created
            };
        }

        public static UserContract ToModel(this UserContractDto userContract)
        {
            return new UserContract
            {
                UserContractId = userContract.UserContractId,
                ContractType = (PersonalFinanceApplication_DomainModels.Enums.ContractType)userContract.ContractType,
                UserId = userContract.UserId,
                ContractName = userContract.ContractName, 
                DateOpened = userContract.DateOpened, 
                UserContractStatus = (PersonalFinanceApplication_DomainModels.Enums.UserContractStatus)userContract.UserContractStatus,
                IsActive = userContract.IsActive,
                Created = userContract.Created
            };
        }
    }
}
