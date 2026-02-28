using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL.Abstraction
{
    public interface IIncomeRepository : IRepository<Income,Guid>
    {
        IQueryable<Income> GetIncomesPerUserContract(int userContractId);
    }
}
