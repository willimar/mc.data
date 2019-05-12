using mc.core.data.Context;
using mc.core.data.Migrations;
using mc.core.domain.register.Entity.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace mc.core.data.test
{
    [TestClass]
    public class DbContextFactoryTest
    {
        [TestMethod]
        public void MigrationTest()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = factory.CreateDbContext(new string[] { }))
                {
                    context.Database.Migrate();

                    var person = context.Set<Person>() as DbSet<Person>;
                    var count = person.Count();

                    Assert.IsTrue(count >= 0);
                }                 
            }
        }
    }
}
