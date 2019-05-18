using mc.provider.sqlserver.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace mc.code.simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            new TopClassSample().PrintLine("Willimar");
            Console.ReadKey();
            Console.WriteLine("Migration sendo iniciado");

            var dbFactory = new DataContext(0, @".\SQLEXPRESS", "MCDATA_TEST", "superwell", "sa");

            //var context = dbFactory.GetDataBse<DbContext>();
            //context.Database.Migrate();
            //context.Database.EnsureCreated();

            Console.ReadKey();
        }
    }
}
