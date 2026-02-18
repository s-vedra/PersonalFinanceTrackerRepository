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
