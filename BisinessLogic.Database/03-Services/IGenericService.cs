using System.Collections.Generic;

namespace BusinessLogic.Database
{
    interface IGenericService<TEntity> where TEntity : class
    {
        public void Add(TEntity item);
        public void Delete(TEntity item);
        public IEnumerable<TEntity> GetData();
        public TEntity GetWithInclude(TEntity item);
        public void Update(TEntity item);
    }
}
