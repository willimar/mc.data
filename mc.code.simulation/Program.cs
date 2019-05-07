using mc.core.data.Context;
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

            var dbFactory = new DbContextFactory();

            var context = dbFactory.CreateDbContext(new string[] {});
            //context.Database.Migrate();
            //context.Database.EnsureCreated();

            Console.ReadKey();
        }
    }
}
