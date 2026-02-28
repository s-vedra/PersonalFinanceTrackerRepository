using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceApplication_Mappers.Mappers
{
    public static class ExpenseMapper
    {
        public static ExpenseDto ToDto(this Expense expense)
        {
            return new ExpenseDto
            {
                ReferenceId = expense.ReferenceId,
                ExpenseId = expense.ExpenseId,
                PaymentIssue = (PaymentIssue)expense.PaymentIssue,
                Amount = expense.Amount,
                Currency = expense.Currency,
                Category = (ExpenseCategory)expense.Category,
                Date = expense.Date,
                Note = expense.Note,
                Purpose = expense.Purpose,
                UserContractId = expense.UserContractId,
                IsActive = expense.IsActive,
                Created = expense.Created
            };
        }

        public static Expense ToModel(this ExpenseDto expense)
        {
            return new Expense
            {
                ReferenceId = expense.ReferenceId,
                ExpenseId = expense.ExpenseId,
                PaymentIssue = (PersonalFinanceApplication_DomainModels.Enums.PaymentIssue)expense.PaymentIssue,
                Amount = expense.Amount,
                Currency = expense.Currency,
                Category = (PersonalFinanceApplication_DomainModels.Enums.ExpenseCategory)expense.Category,
                Date = expense.Date,
                Note = expense.Note,
                Purpose = expense.Purpose,
                UserContractId = expense.UserContractId,
                IsActive = expense.IsActive,
                Created = expense.Created
            };
        }
    }
}
