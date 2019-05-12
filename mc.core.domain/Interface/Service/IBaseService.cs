using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace mc.core.domain.Interface.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        bool IsValidRecord(TEntity entity);
        void UpdateData(TEntity entity);
        TEntity AppenData(TEntity entity);
        TEntity DeleteData(TEntity entity);
        IEnumerable<TEntity> GetData(Expression<Func<TEntity, bool>> func);
        void Dispose();
    }
}
