using mc.core.domain.register.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace mc.core.data.Context
{
    public class DbContextFactory : IDesignTimeDbContextFactory<InternalDbContext>, IDisposable
    {
        private const string CONNECTIONSTRING = @"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
        private string _instance = @".\SQLEXPRESS";
        private object _dataBase = "MCDATA";
        private object _user = "sa";
        private object _password = "superwell";

        public DbContextFactory()
        {
        }


        /// <summary>
        /// use Data Source, Initial Catalog, User and Password in the parameters
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public InternalDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<InternalDbContext>();
            var connectionString = string.Format(CONNECTIONSTRING, this._instance, this._dataBase, this._user, this._password);

            builder.UseSqlServer(connectionString);
            var context = new InternalDbContext(builder.Options);

            context.Database.Migrate();
            context.Database.EnsureCreated();
            var migrations = context.Database.GetAppliedMigrations();

            return context;
        }

        public void Dispose()
        {

        }

        InternalDbContext IDesignTimeDbContextFactory<InternalDbContext>.CreateDbContext(string[] args)
        {
            return this.CreateDbContext(args) as InternalDbContext;
        }
    }
}
