using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;

namespace PersonalFinanceApplication_Mappers.Mappers
{
    public static class ExpenseMapper
    {
        public static ExpenseDto ToDto(this Expense expense)
        {
            return new ExpenseDto
            {
                ExpenseId = expense.ExpenseId,
                PaymentIssue = expense.PaymentIssue,
                Amount = expense.Amount,
                Currency = expense.Currency,
                Category = expense.Category,
                Date = expense.Date,
                Note = expense.Note,
                Purpose = expense.Purpose,
                UserContractId = expense.UserContractId
            };
        }

        public static Expense ToModel(this ExpenseDto expense)
        {
            return new Expense
            {
                ExpenseId = expense.ExpenseId,
                PaymentIssue = expense.PaymentIssue,
                Amount = expense.Amount,
                Currency = expense.Currency,
                Category = expense.Category,
                Date = expense.Date,
                Note = expense.Note,
                Purpose = expense.Purpose,
                UserContractId = expense.UserContractId
            };
        }
    }
}
