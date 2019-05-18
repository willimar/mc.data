using mc.core.repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace mc.provider.sqlserver.Context
{
    internal class DataSet<TEntity> : ISet<TEntity> where TEntity : class
    {
        private DataSet() { }
        public DataSet(InternalContext dbContext)
        {
            this.DbSet = dbContext.Set<TEntity>();
        }

        protected virtual DbSet<TEntity> DbSet { get; set; }

        public TEntity Add(TEntity entity)
        {
            var entry = this.DbSet.Add(entity);
            return entry.Entity;
        }

        public void Dispose()
        {
            DbSet = null;
        }

        public TEntity Remove(TEntity entity)
        {
            var entry = DbSet.Remove(entity);
            return entry.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            var entry = DbSet.Update(entity);
            return entry.Entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }
    }
}
