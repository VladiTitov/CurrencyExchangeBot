using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database
{
    class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        public void Add(TEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetData()
        {
            throw new NotImplementedException();
        }

        public TEntity GetWithInclude(TEntity item)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
