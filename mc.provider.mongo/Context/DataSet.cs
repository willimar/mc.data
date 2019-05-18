using System;
using System.Linq;
using System.Linq.Expressions;
using mc.core.exception.ValuesException;
using mc.core.repository;
using MongoDB.Driver;

namespace mc.provider.mongo.Context
{
    internal class DataSet<TEntity> : ISet<TEntity> where TEntity : class, new()
    {
        protected virtual IMongoCollection<TEntity> DbSet { get; set; }

        private DataSet() { }
        public DataSet(IMongoDatabase provider)
        {
            this.DbSet = provider.GetCollection<TEntity>(typeof(TEntity).ToString());
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

        public TEntity Add(TEntity entity)
        {
            this.IsValidEntity(entity);

            this.DbSet.InsertOne(entity);

            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            dynamic ent = entity;
            Func<dynamic, bool> isEqual = (dynamic item) =>
            {
                var valA = item.Id;
                var valB = ent.Id;

                return valA.Equals(valB);
            };

            this.DbSet.DeleteOne(e => isEqual(e));

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            var entry = this.DbSet.ReplaceOne(e => e.Equals(entity), entity);
            return entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = this.DbSet.Find(predicate);
                return result.ToList().AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            this.DbSet = null;
        }
    }
}
