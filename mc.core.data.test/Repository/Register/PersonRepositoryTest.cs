using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.core.repository;
using mc.provider.sqlserver.Context;
using mc.repository.person;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mc.core.data.test.Repository.Register
{
    [TestClass]
    public class PersonRepositoryTest
    {
        private IProvider GetContext()
        {
            var context = new DataContext(0, @".\SQLEXPRESS", "MCDATA_TEST", "superwell", "sa");
            return context;
        }

        [TestMethod]
        public void PersonIncludeTest()
        {
            var person = GetPerson();

            using (var personRepository = new PersonRepository(GetContext()))
            {
                personRepository.AppenData(person);
                var check = personRepository.GetData(c => c.Id == person.Id);

                Assert.IsTrue(check != null);
                Assert.IsTrue(check.ToList().Any());
            }
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
                list.Add(new PersonalContact() {
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
                list.Add(new Document(){
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
                list.Add(new Address() {
                    City = "Florianópolis",
                    Country = "Brasil",
                    State = "Santa Catarina",
                    Complement = "Near to shopping",
                    District = "Broklyng",
                    Number = "12",
                    PostalCode = "36.036-000",
                    PublicPlace = "Virgilio Varzea"
                });
            }

            return list;
        }
    }
}
