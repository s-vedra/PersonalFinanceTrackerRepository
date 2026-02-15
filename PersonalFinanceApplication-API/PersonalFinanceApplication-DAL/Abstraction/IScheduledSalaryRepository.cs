using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL.Abstraction
{
    public interface IScheduledSalaryRepository : IRepository<SalaryScheduler, Guid>
    {
        IQueryable<SalaryScheduler> GetActiveSalarySchedulers();
    }
}

