using mc.core.domain.register.Entity;
using mc.core.repository;
using mc.provider.mysql.Context;
using mc.repository.person;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace mc.core.data.test.MySql.Register
{
    [TestClass]
    public class CityRepositoryTest
    {
        private IProvider GetContext()
        {
            var context = new DataContext(3306, @"localhost", "mcdata_test", "userTest", "userTest");
            return context;
        }

        [TestMethod]
        public void TesteNewCity()
        {
            var city = new City()
            {
                Code = null,
                Initials = "JF",
                Name = "Juiz de Fora",
                State = null
            };

            using (var cityRepository = new CityRepository(GetContext()))
            {
                cityRepository.AppenData(city);
                var check = cityRepository.GetData(c => c.Id == city.Id);

                Assert.IsTrue(check != null);
                Assert.IsTrue(check.ToList().Any());
            }
        }
    }
}
