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
        public void AddEntity(Expense entity)
        {
            _dataContext.Expenses.Add(entity);
            _dataContext.SaveChanges();
        }

        public IEnumerable<Expense> GetAllEntities()
        {
            return _dataContext.Expenses;
        }

        public Expense GetEntity(int id)
        {
            return _dataContext.Expenses.FirstOrDefault(x => x.ExpenseId == id);
        }

        public void UpdateEntity(Expense currentEntity, Expense updatedEntity)
        {
            _dataContext.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
            _dataContext.SaveChanges();
        }

        public void DeleteEntity(Expense entity)
        {
            _dataContext.Expenses.Remove(entity);
            _dataContext.SaveChanges();
        }
    }
}
