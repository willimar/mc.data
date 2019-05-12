using mc.core.domain.register.Interface.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mc.core.mongo.Context
{
    public class DataContext : IProvider, IDisposable
    {
        public int Port { get; private set; }
        public string Ip { get; private set; }
        public string DataBaseName { get; private set; }
        public string Password { private get; set; }
        public string UserName { private get; set; }

        public DataContext() { }
        public DataContext(int port, string ip, string dataBaseName, string password, string userName)
        {
            this.Port = port;
            this.Ip = ip;
            this.DataBaseName = dataBaseName;
            this.Password = password;
            this.UserName = userName;
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

        public T GetDataBse<T>() 
        {
            return (T)this.CreateContext();
        }

        public void Dispose()
        {

        }
    }
}
