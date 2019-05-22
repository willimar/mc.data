using mc.core.repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.provider.mongo.Context
{
    public class DataContext : IProvider
    {
        private IMongoDatabase _context;

        public int Port { get; private set; }
        public string Ip { get; private set; }
        public string DataBaseName { get; private set; }
        public string Password { private get; set; }
        public string UserName { private get; set; }

        public DataContext() {
            this.Port = 27017;
            this.Ip = "localhost";
            this.DataBaseName = "mctest";
            this.UserName = string.Empty;
            this.Password = string.Empty;
        }
        public DataContext(int port, string ip, string dataBaseName, string password, string userName) {
            this.Port = port;
            this.Ip = ip;
            this.DataBaseName = dataBaseName;
            this.UserName = userName;
            this.Password = password;
            this.DataBasePrepare();
        }

        private IMongoDatabase CreateContext()
        {
            if (string.IsNullOrEmpty(this.Ip))
            {
                throw new InvalidProgramException(nameof(this.Ip));
            }

            if (string.IsNullOrEmpty(this.DataBaseName))
            {
                throw new InvalidProgramException(nameof(this.DataBaseName));
            }

            var connectionString = string.Empty;

            if (string.IsNullOrEmpty(this.UserName))
            {
                connectionString = $"mongodb://{this.Ip}:{this.Port.ToString()}";
            }
            else
            {
                connectionString = $"mongodb://{this.UserName}:{this.Password}@{this.Ip}:{this.Port.ToString()}";
            }

            var client = new MongoClient(connectionString);
            return client.GetDatabase(DataBaseName);
        }

        public void DataBasePrepare()
        {
            this._context = this.CreateContext();
        }

        public void Dispose()
        {
            this._context = null;
        }

        public core.repository.ISet<TEntity> GetSet<TEntity>() where TEntity : class, new()
        {
            return new DataSet<TEntity>(this._context);
        }

        public void SaveChanges(){ }

        public TContext GetContext<TContext>()
        {
            return (dynamic)this._context;
        }
    }
}
