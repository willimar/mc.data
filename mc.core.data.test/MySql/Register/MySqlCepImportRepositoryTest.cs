using mc.core.repository;
using mc.provider.mysql.Context;
using mc.repository.cep;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mc.core.data.test.MySql.Register
{
    [TestClass]
    public class MySqlCepImportRepositoryTest
    {
        private IProvider GetContext()
        {
            var context = new DataContext(3306, @"localhost", "mcdata", "userTest", "userTest");
            return context;
        }
        [TestMethod]
        public void SelectOneRecordTest()
        {
            using (var provider = GetContext())
            {
                using (var repository = new MySqlCepImportRepository(provider))
                {
                    var check = repository.GetData(c => c.Cep.Equals("36038-000"));
                    Assert.IsNotNull(check);
                    Assert.IsTrue(check.Any());
                    Assert.IsTrue(check.Count() == 1);
                }
            }

        }
    }
}
