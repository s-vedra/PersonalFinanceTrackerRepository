namespace PersonalFinanceApplication_DAL.Abstraction
{
    public interface IRepository<T, K>
    {
        T GetEntity(K id);

        K AddEntity(T entity);

        void UpdateEntity(T currentEntity, T updatedEntity);

        IQueryable<T> GetAllEntities();

        void DeleteEntity(T entity);
    }
}
