using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataBaseLayer
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseDbModel
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context) => _context = context;

        public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] include)
        {
            var entities = _context.Set<TEntity>().AsQueryable();
            entities = include.Aggregate(entities, (current, expr) => current.Include(expr));
            return await entities.FirstOrDefaultAsync(i => i.Id.Equals(id)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] include)
        {
            var entities = _context.Set<TEntity>().AsQueryable();
            entities = include.Aggregate(entities, (current, expr) => current.Include(expr));
            return await entities.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, bool>>[] include)
        {
            var entities = _context.Set<TEntity>().AsQueryable();
            entities = include.Aggregate(entities, (current, expr) => current.Include(expr)).Where(filter);
            return await entities.ToListAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id).ConfigureAwait(false);
            Delete(item);
        }

        public virtual void Add(TEntity item) => _context.Set<TEntity>().Add(item);

        public void Delete(TEntity item) => _context.Set<TEntity>().Remove(item);

        public void Update(TEntity item) =>_context.Entry(item).State = EntityState.Modified;

        public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().AsNoTracking().ToList();

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate) => _context.Set<TEntity>().AsNoTracking().Where(predicate).ToList();

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties) => Include(includeProperties).ToList();

        public TEntity GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties) => 
            Include(includeProperties).FirstOrDefault(predicate);

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties) => 
            includeProperties.Aggregate(_context.Set<TEntity>().AsNoTracking(),
                (current, includeProperty) => current.Include(includeProperty));

        public void Dispose() => _context.Dispose();
    }
}
