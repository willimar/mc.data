using mc.cep.service.Providers;
using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.navigator;
using mc.repository.person;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mc.cep.service.test
{
    [TestClass]
    public class CepServiceTest
    {
        [TestMethod]
        public void GetTest()
        {
            var navigator = new NavigatorService(new System.Net.Http.HttpClient());
            var get = new CepService(navigator, new Viacep(), new AddressRepository(new mc.provider.mongo.Context.DataContext()));
            var address = get.Get("36038000");
            var addressCheck = new Address() {
                PostalCode = "36038-000",
                PublicPlace = "Rua Professor Villas Bouçada",
                District = "Santos Dumont",
                City = "Juiz de Fora",
                State = "MG",
                Country = "Brasil"
            };

            Assert.IsTrue(address.Equals(addressCheck));
        }
    }
}
