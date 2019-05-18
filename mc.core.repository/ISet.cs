using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace mc.core.repository
{
    public interface ISet<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity Remove(TEntity entity);
        TEntity Update(TEntity entity);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        void Dispose();
    }
}
