using mc.core.domain.register.Entity;
using mc.core.repository;
using mc.provider.sqlserver.Context;
using mc.repository.person;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace mc.core.data.test.Repository.Register
{
    [TestClass]
    public class CityRepositoryTest
    {
        private IProvider GetContext()
        {
            var context = new DataContext(0, @".\SQLEXPRESS", "MCDATA_TEST", "superwell", "sa");
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
                State = new State()
                {
                    Code = null,
                    Initials = "MG",
                    Name = "Minas Gerais",
                    Country = new Country()
                    {
                        Code = null,
                        Initials = "BRA",
                        Name = "Brasil"
                    }
                }
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
