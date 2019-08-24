using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Portal.core.Infrastructure;
using Portal.core;

namespace Portal.Standard.Infrastructure
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly PortalDbContext _portalContext;

        public EFRepository(PortalDbContext portalContext)
        {
            _portalContext = portalContext;
        }
        public T GetById(object id)
        {
            return _portalContext.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            _portalContext.Set<T>().Add(entity);
            _portalContext.SaveChanges();
        }

        public void Insert(IEnumerable<T> entities)
        {
            _portalContext.Set<T>().AddRange(entities);
            _portalContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _portalContext.SaveChanges();
        }

        public void Update(IEnumerable<T> entities)
        {
            _portalContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _portalContext.Set<T>().Remove(entity);
            _portalContext.SaveChanges();
        }

        public void Delete(IEnumerable<T> entities)
        {
            _portalContext.Set<T>().RemoveRange(entities);
            _portalContext.SaveChanges();
        }

        public IQueryable<T> Table => _portalContext.Set<T>();

        public IQueryable<T> TableNoTracking => _portalContext.Set<T>().AsNoTracking();
    }
}
