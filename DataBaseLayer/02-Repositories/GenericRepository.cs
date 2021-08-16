﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataBaseLayer
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

        public virtual void Add(TEntity item) =>_dbSet.Add(item);

        public void Delete(TEntity item) =>_dbSet.Remove(item);

        public void Update(TEntity item) =>_context.Entry(item).State = EntityState.Modified;

        public IEnumerable<TEntity> GetAll() => _dbSet.AsNoTracking().ToList();

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate) => _dbSet.AsNoTracking().Where(predicate).ToList();

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties) => Include(includeProperties).ToList();

        public TEntity GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties) => 
            Include(includeProperties).FirstOrDefault(predicate);

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties) => 
            includeProperties.Aggregate(_dbSet.AsNoTracking(),
                (current, includeProperty) => current.Include(includeProperty));

        public void Dispose() { }
    }
}