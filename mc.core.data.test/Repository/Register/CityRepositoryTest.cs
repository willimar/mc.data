using mc.core.data.Context;
using mc.core.data.Repository.Register;
using mc.core.domain.register.Entity;
using mc.core.domain.register.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mc.core.data.test.Repository.Register
{
    [TestClass]
    public class CityRepositoryTest
    {
        private IProvider GetContext()
        {
            var context = new DbContextFactory().CreateDbContext(new string[] { @".\SQLEXPRESS", "MCDATA_TEST", "sa", "superwell" });
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
