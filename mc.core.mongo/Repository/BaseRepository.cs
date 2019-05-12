using mc.core.domain.Interface.Repository;
using mc.core.domain.register.Interface.Repository;
using mc.core.exception.ValuesException;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace mc.core.mongo.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity : class, new()
    {
        private IMongoDatabase provider;
        private readonly IMongoCollection<TEntity> dbSet;

        private BaseRepository()
        {

        }

        public BaseRepository(IProvider provider)
        {
            this.provider = provider.GetDataBse<IMongoDatabase>();
            this.dbSet = this.provider.GetCollection<TEntity>(typeof(TEntity).ToString());
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

            this.dbSet.InsertOne(entity);

            return entity;
        }

        public TEntity DeleteData(TEntity entity)
        {
            dynamic ent = entity;
            Func<dynamic, bool> isEqual = (dynamic item) =>
            {
                var valA = item.Id;
                var valB = ent.Id;

                return valA.Equals(valB);
            };

            this.dbSet.DeleteOne(e => isEqual(e));

            return entity;
        }

        public void Dispose()
        {
            
        }

        public IEnumerable<TEntity> GetData(Expression<Func<TEntity, bool>> func)
        {
            var result = this.dbSet.Find(func);
            return result.ToList();
        }

        public void UpdateData(TEntity entity)
        {
            this.dbSet.ReplaceOne(e => e.Equals(entity), entity);
        }
    }
}
