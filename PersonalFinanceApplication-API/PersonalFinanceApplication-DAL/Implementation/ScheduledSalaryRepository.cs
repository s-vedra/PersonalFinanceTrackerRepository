using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL.Implementation
{
    public class ScheduledSalaryRepository : IScheduledSalaryRepository
    {
        private readonly DataContext _dataContext;
        public ScheduledSalaryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Guid AddEntity(SalaryScheduler entity)
        {
            _dataContext.ScheduledSalaries.Add(entity);
            _dataContext.SaveChanges();
            return entity.ReferenceId;
        }

        public void DeleteEntity(SalaryScheduler entity)
        {
            _dataContext.ScheduledSalaries.Remove(entity);
            _dataContext.SaveChanges();
        }

        public IQueryable<SalaryScheduler> GetActiveSalarySchedulers()
        {
            return _dataContext.ScheduledSalaries.Where(x => x.DayOfMonth == DateTime.Today.Day);
        }

        public IEnumerable<SalaryScheduler> GetAllEntities()
        {
            return _dataContext.ScheduledSalaries;
        }

        public SalaryScheduler GetEntity(Guid id)
        {
            return _dataContext.ScheduledSalaries.FirstOrDefault(x => x.ReferenceId == id);
        }

        public void UpdateEntity(SalaryScheduler currentEntity, SalaryScheduler updatedEntity)
        {
            _dataContext.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
            _dataContext.SaveChanges();
        }
    }
}
