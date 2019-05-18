using mc.core.domain.Interface.Repository;
using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.repository;
using mc.core.service.Interface;
using mc.core.service.Register;
using mc.provider.sqlserver.Context;
using mc.repository.person;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mc.core.service.test.Register
{
    [TestClass]
    public class PersonServiceTest
    {
        private IPersonService GetPersonService()
        {
            return new PersonService(GetPersonRepository() as IPersonRepository);
        }

        private IBaseRepository<Person> GetPersonRepository()
        {
            return new PersonRepository(GetContext());
        }

        private IProvider GetContext()
        {
            var context = new DataContext(0, @".\SQLEXPRESS", "MCDATA_TEST", "superwell", "sa");
            return context;
        }

        private Person GetPerson(bool isDependent = false)
        {
            var complement = isDependent ? "Dependent" : "Principal";

            return new Person()
            {
                BirthDate = new DateTime(2019, 04, 05),
                Gender = Gender.male,
                Name = $"Teste Register Person [{complement}]",
                Addresses = GetAddresses(),
                Dependents = isDependent ? null : GetDependents(),
                Documents = GetDocuments(),
                PersonalContacts = GetPersonalContacts()
            };
        }

        private IEnumerable<PersonalContact> GetPersonalContacts()
        {
            var list = new List<PersonalContact>(5);
            for (int i = 0; i < 5; i++)
            {
                list.Add(new PersonalContact()
                {
                    ContactType = ContactTypes.celphone,
                    Value = @"(48) 9 9649-2933"
                });
            }

            return list;
        }

        private IEnumerable<Document> GetDocuments()
        {
            var list = new List<Document>(5);
            for (int i = 0; i < 5; i++)
            {
                list.Add(new Document()
                {
                    Complement = "COMPLEMENT",
                    EmissionDate = DateTime.Now,
                    Name = "DOCUMENT NAME",
                    Value = "558-7778/998-213.45"
                });
            }

            return list;
        }

        private IEnumerable<Person> GetDependents()
        {
            var list = new List<Person>(5);
            for (int i = 0; i < 5; i++)
            {
                list.Add(GetPerson(true));
            }

            return list;
        }

        private IEnumerable<Address> GetAddresses()
        {
            var list = new List<Address>(5);
            for (int i = 0; i < 5; i++)
            {
                list.Add(new Address()
                {
                    City = GetCity(),
                    Complement = "Near to shopping",
                    District = "Broklyng",
                    Number = "12",
                    PostalCode = "36.036-000",
                    PublicPlace = "Virgilio Varzea"
                });
            }

            return list;
        }

        private City GetCity()
        {
            return new City()
            {
                Code = null,
                Initials = "FLR",
                Name = "Florianópolis",
                State = new State()
                {
                    Code = null,
                    Initials = "SC",
                    Name = "Santa Catarina",
                    Country = new Country()
                    {
                        Code = null,
                        Initials = "BRA",
                        Name = "Brasil"
                    }
                }
            };
        }

        [TestMethod]
        public void IsValidRecordTest()
        {
            IPersonService personService = GetPersonService();

            Assert.IsTrue(personService.IsValidRecord(GetPerson()));
        }

        [TestMethod]
        public void IsInvalidRecordTest()
        {
            IPersonService personService = GetPersonService();

            Assert.IsFalse(personService.IsValidRecord(new Person()));
        }

        [TestMethod]
        public void UpdateDataTest()
        {
            var service = GetPersonService();
            var person = GetPerson();
            service.AppenData(person);
            service.UpdateData(person);
        }

        [TestMethod]
        public void AppenDataTest()
        {
            var service = GetPersonService();
            service.AppenData(GetPerson());
        }

        [TestMethod]
        public void DeleteDataTest()
        {
            var service = GetPersonService();
            var person = GetPerson();
            service.AppenData(person);
            service.DeleteData(person);
        }

        [TestMethod]
        public void GetDataTest()
        {
            var service = GetPersonService();
            var person = GetPerson();
            service.AppenData(person);
            var getPerson = service.GetData(x => x.Id.Equals(person.Id));

            Assert.IsTrue(getPerson.Any());
        }
    }
}
