using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Portal.core;
using Portal.core.Infrastructure;

namespace Portal.Infrastructure
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly PortalDbContext _marketingContext;

        public EFRepository(PortalDbContext marketingContext)
        {
            _marketingContext = marketingContext;
        }
        public T GetById(object id)
        {
            return _marketingContext.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            _marketingContext.Set<T>().Add(entity);
            _marketingContext.SaveChanges();
        }

        public void Insert(IEnumerable<T> entities)
        {
            _marketingContext.Set<T>().AddRange(entities);
            _marketingContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _marketingContext.SaveChanges();
        }

        public void Update(IEnumerable<T> entities)
        {
            _marketingContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _marketingContext.Set<T>().Remove(entity);
            _marketingContext.SaveChanges();
        }

        public void Delete(IEnumerable<T> entities)
        {
            _marketingContext.Set<T>().RemoveRange(entities);
            _marketingContext.SaveChanges();
        }

        public IQueryable<T> Table => _marketingContext.Set<T>();

        public IQueryable<T> TableNoTracking => _marketingContext.Set<T>().AsNoTracking();
    }
}
