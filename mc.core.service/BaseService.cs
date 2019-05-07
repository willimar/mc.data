using mc.core.domain.Interface.Repository;
using mc.core.domain.Interface.Service;
using mc.core.exception.ValuesException;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.service
{
    public class BaseService<TEntity> : IBaseService<TEntity>, IDisposable where TEntity : class, new()
    {
        public IBaseRepository<TEntity> repositoryBase = null;

        private BaseService() { }

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            this.repositoryBase = baseRepository;
        }

        public TEntity AppenData(TEntity entity)
        {
            if (!this.IsValidRecord(entity))
            {
                throw new InvalidValueException(nameof(entity));
            }

            return this.repositoryBase.AppenData(entity);
        }

        public TEntity DeleteData(TEntity entity)
        {
            if (!this.IsValidRecord(entity))
            {
                throw new InvalidValueException(nameof(entity));
            }

            return this.repositoryBase.DeleteData(entity);
        }

        public void Dispose()
        {
            this.repositoryBase.Dispose();
        }

        public IEnumerable<TEntity> GetData(Func<TEntity, bool> func)
        {
            return this.repositoryBase.GetData(func);
        }

        public virtual bool IsValidRecord(TEntity entity)
        {
            if (entity.Equals(new TEntity()))
            {
                return false;
            }

            return true;
        }

        public void UpdateData(TEntity entity)
        {
            if (!this.IsValidRecord(entity))
            {
                throw new InvalidValueException(nameof(entity));
            }

            this.repositoryBase.UpdateData(entity);
        }
    }
}
