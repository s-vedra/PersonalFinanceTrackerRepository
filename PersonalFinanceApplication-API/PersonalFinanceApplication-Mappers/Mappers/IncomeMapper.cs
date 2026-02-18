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
                ReferenceId = income.ReferenceId,
                IncomeId = income.IncomeId,
                PaymentIssue = income.PaymentIssue,
                Amount = income.Amount,
                Currency = income.Currency,
                Category = income.Category,
                Date = income.Date,
                Note = income.Note,
                Purpose = income.Purpose,
                UserContractId = income.UserContractId,
                IsActive = income.IsActive,
                Created = income.Created
            };
        }

        public static Income ToModel(this IncomeDto income)
        {
            return new Income
            {
                ReferenceId = income.ReferenceId,
                IncomeId = income.IncomeId,
                PaymentIssue = income.PaymentIssue,
                Amount = income.Amount,
                Currency = income.Currency,
                Category = income.Category,
                Date = income.Date,
                Note = income.Note,
                Purpose = income.Purpose,
                UserContractId = income.UserContractId,
                IsActive = income.IsActive,
                Created = income.Created
            };
        }
    }
}
