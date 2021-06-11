using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.DataBaseLayer
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity bank);
        void Delete(TEntity item);
        void Update(TEntity bank);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

        public TEntity GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        void Dispose();
    }
}



