using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int AddEntity(User entity)
        {
            _dataContext.Users.Add(entity);
            _dataContext.SaveChanges();
            return entity.UserId;
        }

        public void DeleteEntity(User entity)
        {
            entity.IsActive = false;
            _dataContext.Users.Update(entity);
            _dataContext.SaveChanges();
        }

        public IQueryable<User> GetAllEntities()
        {
            return _dataContext.Users.Where(x => x.IsActive);
        }

        public User GetEntity(int id)
        {
            return _dataContext.Users.FirstOrDefault(x => x.UserId == id && x.IsActive);
        }

        public void UpdateEntity(User currentEntity, User updatedEntity)
        {
            _dataContext.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
            _dataContext.SaveChanges();
        }
    }
}
