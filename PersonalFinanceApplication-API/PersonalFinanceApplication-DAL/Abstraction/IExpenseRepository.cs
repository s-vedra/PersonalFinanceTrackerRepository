using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL.Abstraction
{
    public interface IExpenseRepository : IRepository<Expense, Guid>
    {
        IQueryable<Expense> GetExpendituresPerUserContract(int userContractId);
    }
}
