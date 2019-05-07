using mc.core.domain.Interface.Repository;
using mc.core.domain.register.Interface.Repository;
using mc.core.exception.ValuesException;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mc.core.data.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity : class, new()
    {
        protected readonly IProvider dbContext = null;
        protected readonly DbSet<TEntity> dbSet = null;

        private BaseRepository() { }

        private void IsValidEntity(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentException("No value to save data.");
            }

            if (entity.Equals(new TEntity()))
            {
                throw new InvalidValueException(nameof(TEntity));
            }
        }

        public BaseRepository(IProvider dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = (this.dbContext as DbContext).Set<TEntity>() as DbSet<TEntity>;
        }

        public TEntity DeleteData(TEntity entity)
        {
            IsValidEntity(entity);

            var entry = dbSet.Remove(entity);

            if (entity is null || entry.State != EntityState.Deleted)
            {
                throw new NoChangedValueException();
            }

            (this.dbContext as DbContext).SaveChanges();

            return entry.Entity;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public IEnumerable<TEntity> GetData(Func<TEntity, bool> func)
        {
            var result = dbSet.Where(func);

            if (result is null)
            {
                return new List<TEntity>();
            }
            else
            {
                return result.ToList();
            }
        }

        public void UpdateData(TEntity entity)
        {
            this.IsValidEntity(entity);

            var entry = dbSet.Update(entity);

            if (entry is null)
            {
                throw new ValueNotFoundException();
            }

            if (entry.State != EntityState.Modified)
            {
                throw new NoChangedValueException();
            }

            (this.dbContext as DbContext).SaveChanges();
        }

        public TEntity AppenData(TEntity entity)
        {
            this.IsValidEntity(entity);

            var entry = this.dbSet.Add(entity);

            if (entry is null || entry.State != EntityState.Added)
            {
                throw new NoChangedValueException();
            }

            (this.dbContext as DbContext).SaveChanges();

            return entry.Entity;
        }
    }
}
