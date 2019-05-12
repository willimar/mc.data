using mc.core.data.EntityConfig;
using mc.core.data.EntityConfig.Person;
using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.data.Context
{
    public class DataContext: IProvider
    {
        public int Port { get; set; }
        public string Ip { get; set; }
        public string DataBaseName { get; set; }
        public string Password { private get; set; }
        public string UserName { private get; set; }

        public DataContext()
        {
            this.Port = 0;
            this.Ip = @".\SQLEXPRESS";
            this.DataBaseName = "MCDATA";
            this.Password = "superwell";
            this.UserName = "sa";
        }
        public DataContext(int port, string ip, string dataBaseName, string password, string userName)
        {
            this.Port = port;
            this.Ip = ip;
            this.DataBaseName = dataBaseName;
            this.Password = password;
            this.UserName = userName;
        }

        public T GetDataBse<T>() 
        {
            const string CONNECTIONSTRING = @"Data Source={0}{4};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
            var builder = new DbContextOptionsBuilder();
            var connectionString = string.Format(CONNECTIONSTRING, this.Ip, this.DataBaseName, this.UserName, this.Password,
                    this.Port > 0 ? this.Port.ToString(",0") : string.Empty);
            builder.UseSqlServer(connectionString);
            var context = new InternalDbContext(builder.Options);

            return (dynamic)context;
        }

        public void Dispose()
        {
            
        }
    }
}
