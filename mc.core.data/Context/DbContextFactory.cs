using mc.core.domain.register.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace mc.core.data.Context
{
    public class DbContextFactory: IDesignTimeDbContextFactory<DataContext>
    {
        private const string CONNECTIONSTRING = @"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";

        public DbContextFactory()
        {
        }
        

        /// <summary>
        /// use Data Source, Initial Catalog, User and Password in the parameters
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public IProvider CreateDbContext(string[] args)
        {
            if ((args is null) || (args.Length != 4))
            {
                throw new ArgumentException("Number of the parameters is invalid.");
                //args = new string[] { @".\SQLEXPRESS", "MCDATA_TEST", "sa", "*****" };
            }

            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = string.Format(CONNECTIONSTRING, args[0], args[1], args[2], args[3]);

            builder.UseSqlServer(connectionString);

            return new DataContext(builder.Options);
        }

        DataContext IDesignTimeDbContextFactory<DataContext>.CreateDbContext(string[] args)
        {
            return this.CreateDbContext(args) as DataContext;
        }
    }
}
