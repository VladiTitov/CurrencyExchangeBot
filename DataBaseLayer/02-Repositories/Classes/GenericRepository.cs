using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataBaseLayer._02_Repositories.Interfaces
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public void Delete(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll() => _dbSet.AsNoTracking().ToList();

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking()
                .Where(predicate)
                .ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties) =>
            Include(includeProperties).ToList();

        public TEntity GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).FirstOrDefault();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));
        }

        public void Dispose() { }
    }
}
