using PFA_DAL.Abstraction;
using PFA_DM.Models;

namespace PFA_DAL.Implementation
{
    public class AccountBalanceRepository : IAccountBalanceRepository
    {
        private readonly DataContext _dataContext;

        public AccountBalanceRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddEntity(AccountBalance entity)
        {
            _dataContext.AccountBalances.Add(entity);
            _dataContext.SaveChanges();
        }

        public void DeleteEntity(AccountBalance entity)
        {
            entity.IsActive = false;
            _dataContext.AccountBalances.Update(entity);
            _dataContext.SaveChanges();
        }

        public IQueryable<AccountBalance> GetAllEntities()
        {
            return _dataContext.AccountBalances.Where(x => x.IsActive);
        }

        public AccountBalance GetEntity(int id)
        {
            return _dataContext.AccountBalances.FirstOrDefault(x => x.UserContractId == id && x.IsActive);
        }

        public void UpdateEntity(AccountBalance currentEntity, AccountBalance updatedEntity)
        {
            _dataContext.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
            _dataContext.SaveChanges();
        }
    }
}
