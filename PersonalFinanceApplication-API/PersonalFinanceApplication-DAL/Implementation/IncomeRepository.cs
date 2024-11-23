using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL.Implementation
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly DataContext _dataContext;

        public IncomeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int AddEntity(Income entity)
        {
            _dataContext.Incomes.Add(entity);
            _dataContext.SaveChanges();
            return entity.IncomeId;
        }

        public IEnumerable<Income> GetAllEntities()
        {
            return _dataContext.Incomes;
        }

        public Income GetEntity(int id)
        {
            return _dataContext.Incomes.FirstOrDefault(x => x.IncomeId == id);
        }

        public void UpdateEntity(Income currentEntity, Income updatedEntity)
        {
            _dataContext.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
            _dataContext.SaveChanges();
        }

        public void DeleteEntity(Income entity)
        {
            _dataContext.Incomes.Remove(entity);
            _dataContext.SaveChanges();
        }
    }
}
