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
        public int AddEntity(UserContract entity)
        {
            _dataContext.UserContracts.Add(entity);
            _dataContext.SaveChanges();
            return entity.UserContractId;
        }

        public void DeleteEntity(UserContract entity)
        {
            entity.IsActive = false;
            _dataContext.UserContracts.Update(entity);
            _dataContext.SaveChanges();
        }

        public IQueryable<UserContract> GetAllEntities()
        {
            return _dataContext.UserContracts.Where(x => x.IsActive);
        }

        public UserContract GetEntity(int id)
        {
            return _dataContext.UserContracts.FirstOrDefault(x => x.UserContractId == id && x.IsActive);
        }

        public void UpdateEntity(UserContract currentEntity, UserContract updatedEntity)
        {
            _dataContext.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
            _dataContext.SaveChanges();
        }
    }
}
