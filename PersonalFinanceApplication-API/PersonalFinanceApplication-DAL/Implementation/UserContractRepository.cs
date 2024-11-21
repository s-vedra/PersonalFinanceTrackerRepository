using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL.Implementation
{
    public class UserContractRepository : IUserContractRepository
    {
        private readonly DataContext _dataContext;

        public UserContractRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddEntity(UserContract entity)
        {
            _dataContext.UserContracts.Add(entity);
            _dataContext.SaveChanges();
        }

        public void DeleteEntity(UserContract entity)
        {
            _dataContext.UserContracts.Remove(entity);
            _dataContext.SaveChanges();
        }

        public IEnumerable<UserContract> GetAllEntities()
        {
            return _dataContext.UserContracts;
        }

        public UserContract GetEntity(int id)
        {
            return _dataContext.UserContracts.FirstOrDefault(x => x.UserContractId == id);
        }

        public void UpdateEntity(UserContract currentEntity, UserContract updatedEntity)
        {
            _dataContext.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
            _dataContext.SaveChanges();
        }
    }
}
