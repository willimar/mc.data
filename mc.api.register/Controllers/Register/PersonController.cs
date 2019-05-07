using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mc.core.domain.register.Entity.Person;
using mc.core.service.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mc.api.register.Controllers.Register
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private IPersonService personService;

        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpGet]
        public ActionResult<string> Index()
        {
            var person = new Person() {
                Addresses = new List<Address>() { new Address() },
                Dependents = new List<Person>(),
                Documents = new List<Document>() { new Document() },
                PersonalContacts = new List<PersonalContact>() { new PersonalContact() }
            };

            var jsonValue = JsonConvert.SerializeObject(person)
                .Replace(",", ",\r\t");
            var result = $"Sample of domain: \r\t{jsonValue}";

            return result;
        }

        [HttpPost]
        public ActionResult<Person> Append(Person person)
        {
            return this.personService.AppenData(person);
        }
    }
}
