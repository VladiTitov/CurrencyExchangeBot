using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.DataBaseLayer
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] include);
        public Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] include);
        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, bool>>[] include);
        public Task DeleteAsync(int id);

        void Add(TEntity item);
        void Delete(TEntity item);
        void Update(TEntity item);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

        public TEntity GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        void Dispose();
    }
}



