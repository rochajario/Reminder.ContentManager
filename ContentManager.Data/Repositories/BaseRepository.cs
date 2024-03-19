using ContentManager.Data.Entities;
using ContentManager.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Data.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly ApplicationContext _applicationContext;

        public BaseRepository(IApplicationContext applicationContext)
        {
            _applicationContext = (ApplicationContext)applicationContext;
        }

        public virtual T Create(T entity)
        {
            var addedItem = _applicationContext.Set<T>().Add(entity).Entity;
            _applicationContext.SaveChanges();
            return addedItem;
        }

        public virtual void Delete(Guid id)
        {
            var itemToRemove = _applicationContext.Set<T>().AsTracking().SingleOrDefault(x => x.Id.Equals(id));
            if (itemToRemove != null)
            {
                _applicationContext.Remove(itemToRemove);
                _applicationContext.SaveChanges();
            }
        }

        public virtual IQueryable<T> ReadAll()
        {
            return _applicationContext.Set<T>().AsNoTracking();
        }

        public virtual T ReadById(Guid id)
        {
            return _applicationContext.Set<T>().AsNoTracking().Single(x => x.Id.Equals(id));
        }

        public virtual void Update(Guid id, T entity)
        {
            var item = _applicationContext.Set<T>()
                .Where(x => x.Id.Equals(id))
                .AsNoTracking()
                .SingleOrDefault();

            if (item != null)
            {
                item = (T)entity.Clone();
                item.Id = id;

                _applicationContext.Set<T>().Update(item);
                _applicationContext.SaveChanges();
            }
        }
    }
}
