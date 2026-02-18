using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL.Implementation
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly DataContext _dataContext;

        public ExpenseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Guid AddEntity(Expense entity)
        {
            _dataContext.Expenses.Add(entity);
            _dataContext.SaveChanges();
            return entity.ReferenceId;
        }

        public IQueryable<Expense> GetAllEntities()
        {
            return _dataContext.Expenses.Where(x => x.IsActive);
        }

        public Expense GetEntity(Guid id)
        {
            return _dataContext.Expenses.FirstOrDefault(x => x.ReferenceId == id && x.IsActive);
        }

        public void UpdateEntity(Expense currentEntity, Expense updatedEntity)
        {
            _dataContext.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
            _dataContext.SaveChanges();
        }

        public void DeleteEntity(Expense entity)
        {
            entity.IsActive = false;
            _dataContext.Expenses.Update(entity);
            _dataContext.SaveChanges();
        }
    }
}
