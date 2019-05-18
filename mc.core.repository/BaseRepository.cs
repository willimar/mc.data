using mc.core.domain.Interface.Repository;
using mc.core.exception.ValuesException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace mc.core.repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity : class, new()
    {
        protected readonly ISet<TEntity> set = null;
        protected readonly IProvider provider = null;

        public BaseRepository(IProvider provider)
        {
            this.provider = provider;
            this.set = this.provider.GetSet<TEntity>();
        }

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

        public TEntity AppenData(TEntity entity)
        {
            this.IsValidEntity(entity);

            var entry = this.set.Add(entity);

            if (entry is null)
            {
                throw new NoChangedValueException();
            }

            this.provider.SaveChanges();

            return entry;
        }

        public TEntity DeleteData(TEntity entity)
        {
            IsValidEntity(entity);

            var entry = set.Remove(entity);

            if (entity is null)
            {
                throw new NoChangedValueException();
            }

            this.provider.SaveChanges();

            return entry;
        }

        public void Dispose()
        {
            set.Dispose();
            provider.Dispose();
        }

        public IEnumerable<TEntity> GetData(Expression<Func<TEntity, bool>> func)
        {
            var result = set.Where(func);

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

            var entry = set.Update(entity);

            if (entry is null)
            {
                throw new ValueNotFoundException();
            }

            if (!entry.Equals(entity))
            {
                throw new NoChangedValueException();
            }

            this.provider.SaveChanges();
        }
    }
}
