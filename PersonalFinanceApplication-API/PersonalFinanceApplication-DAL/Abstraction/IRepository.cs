namespace PersonalFinanceApplication_DAL.Abstraction
{
    public interface IRepository<T, K>
    {
        T GetEntity(K id);

        int AddEntity(T entity);

        void UpdateEntity(T currentEntity, T updatedEntity);

        IEnumerable<T> GetAllEntities();

        void DeleteEntity(T entity);
    }
}
