using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;

namespace PersonalFinanceApplication_Mappers.Mappers
{
    public static class IncomeMapper
    {
        public static IncomeDto ToDto(this Income income)
        {
            return new IncomeDto
            {
                IncomeId = income.IncomeId,
                Account = income.Account,
                Amount = income.Amount,
                Currency = income.Currency,
                Category = income.Category,
                Date = income.Date,
                Note = income.Note,
                Purpose = income.Purpose,
                UserContractId = income.UserContractId
            };
        }

        public static Income ToModel(this IncomeDto income)
        {
            return new Income
            {
                IncomeId = income.IncomeId,
                Account = income.Account,
                Amount = income.Amount,
                Currency = income.Currency,
                Category = income.Category,
                Date = income.Date,
                Note = income.Note,
                Purpose = income.Purpose,
                UserContractId = income.UserContractId
            };
        }
    }
}
