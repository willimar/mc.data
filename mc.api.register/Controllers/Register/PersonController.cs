using mc.core.domain.register.Entity.Person;
using mc.core.service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public ActionResult<Person> PostPerson([FromBody]Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            return this.personService.AppenData(person);
        }
    }
}
