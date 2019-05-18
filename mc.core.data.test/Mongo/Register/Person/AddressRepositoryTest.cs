using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.core.repository;
using mc.provider.mongo.Context;
using mc.repository.person;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Linq;

namespace mc.core.data.test.Mongo.Register.Person
{
    [TestClass]
    public class AddressRepositoryTest
    {
        private IProvider CreateContext()
        {
            IProvider dbContext = new DataContext(27017, "localhost", "mctest", string.Empty, string.Empty);
            return dbContext;
        }

        private Address GetAddress()
        {
            return new Address() {
                PostalCode = "36038-000",
                PublicPlace = "Villa Bolçadas",
                District = "Santos Dumont",
                City = new City()
                {
                    Name = "Juiz de Fora",
                    State = new State()
                    {
                        Name = "Minas Gerais",
                        Country = new Country()
                        {
                            Name = "Brasil"
                        }
                    }
                }
            };
        }

        [TestMethod]
        public void InsertRecordCase1()
        {
            using (var addressRepository = new AddressRepository(CreateContext()))
            {
                var address = GetAddress();
                addressRepository.AppenData(address);
                var check = addressRepository.GetData(e => e.Id.Equals(address.Id));

                Assert.IsTrue(check != null);
                Assert.IsTrue(check.ToList().Any());
            }
        }
    }
}
