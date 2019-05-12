using mc.cep.service.Providers;
using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.navigator;
using mc.navigator.domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mc.cep.service.test
{
    [TestClass]
    public class CepServiceTest
    {
        [TestMethod]
        public void GetTest()
        {
            var navigator = new NavigatorService();
            var get = new CepService<Address>(navigator, new Viacep());
            var address = get.Get("36038000");
            var addressCheck = new Address() {
                PostalCode = "36038-000",
                PublicPlace = "Rua Professor Villas Bouçada",
                District = "Santos Dumont",
                City = new City()
                {
                    Name = "Juiz de Fora",
                    State = new State()
                    {
                        Initials = "MG",
                        Country = new Country()
                        {
                            Name = "Brasil",
                            Initials = "BR"
                        }
                    }
                }
            };

            Assert.IsTrue(address.Equals(addressCheck));
        }
    }
}
