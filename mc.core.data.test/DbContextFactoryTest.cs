using mc.core.data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace mc.core.data.test
{
    [TestClass]
    public class DbContextFactoryTest
    {
        [TestMethod]
        public void MigrationTest()
        {
            using (var context = new DbContextFactory().CreateDbContext(new string[] { @".\SQLEXPRESS", "MCDATA_TEST", "sa", "superwell" }) as DbContext)
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MigrationInvalidArguments1Test()
        {
            using (var context = new DbContextFactory().CreateDbContext(new string[] { }) as DbContext)
            {

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MigrationInvalidArguments2Test()
        {
            using (var context = new DbContextFactory().CreateDbContext(null) as DbContext)
            {
  
            }
        }
    }
}
