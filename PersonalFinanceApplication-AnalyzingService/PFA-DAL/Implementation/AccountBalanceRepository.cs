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
            _dataContext.AccountBalances.Remove(entity);
            _dataContext.SaveChanges();
        }

        public IEnumerable<AccountBalance> GetAllEntities()
        {
            return _dataContext.AccountBalances;
        }

        public AccountBalance GetEntity(int id)
        {
            return _dataContext.AccountBalances.FirstOrDefault(x => x.UserContractId == id);
        }

        public void UpdateEntity(AccountBalance currentEntity, AccountBalance updatedEntity)
        {
            _dataContext.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
            _dataContext.SaveChanges();
        }
    }
}
