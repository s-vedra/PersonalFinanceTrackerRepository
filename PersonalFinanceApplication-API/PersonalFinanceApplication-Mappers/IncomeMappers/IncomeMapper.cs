using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;

namespace PersonalFinanceApplication_Mappers.IncomeMappers
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
                Category = income.Category, 
                Date = income.Date, 
                Note = income.Note, 
                Purpose = income.Purpose
            };
        }

        public static Income ToModel(this IncomeDto income)
        {
            return new Income
            {
                IncomeId = income.IncomeId,
                Account = income.Account,
                Amount = income.Amount,
                Category = income.Category,
                Date = income.Date,
                Note = income.Note,
                Purpose = income.Purpose
            };
        }
    }
}
