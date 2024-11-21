namespace PFA_DAL.Abstraction
{
    public interface IRepository<T, K>
    {
        T GetEntity(K id);

        void AddEntity(T entity);

        void UpdateEntity(T currentEntity, T updatedEntity);

        IEnumerable<T> GetAllEntities();

        void DeleteEntity(T entity);
    }
}
