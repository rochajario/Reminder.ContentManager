using ContentManager.Data.Entities;

namespace ContentManager.Data.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        T Create(T entity);
        T ReadById(Guid id);
        IQueryable<T> ReadAll();
        void Update(Guid id, T entity);
        void Delete(Guid id);
    }
}
