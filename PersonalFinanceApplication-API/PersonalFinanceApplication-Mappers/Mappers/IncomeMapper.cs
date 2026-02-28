using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

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
                PaymentIssue = (PaymentIssue)income.PaymentIssue,
                Amount = income.Amount,
                Currency = income.Currency,
                Category = (IncomeCategory)income.Category,
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
                PaymentIssue = (PersonalFinanceApplication_DomainModels.Enums.PaymentIssue)income.PaymentIssue,
                Amount = income.Amount,
                Currency = income.Currency,
                Category = (PersonalFinanceApplication_DomainModels.Enums.IncomeCategory)income.Category,
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
