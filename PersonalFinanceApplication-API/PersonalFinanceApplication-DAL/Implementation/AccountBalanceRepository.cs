using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL.Implementation
{
    public class AccountBalanceRepository : IAccountBalanceRepository
    {
        private readonly DataContext _dataContext;

        public AccountBalanceRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int AddEntity(AccountBalance entity)
        {
            _dataContext.AccountBalances.Add(entity);
            _dataContext.SaveChanges();
            return entity.AccountBalanceId;
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
