using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mc.cep.service;
using mc.core.domain.register.Entity.Person;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mc.cep.check.Controllers
{
    [EnableCors("_AllowSpecificOrigins")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CepController : Controller
    {
        private CepService<Address> cepService;

        public class Cep
        {
            public string Value { get; set; }
        }

        public CepController(CepService<Address> cepService)
        {
            this.cepService = cepService;
        }

        [HttpGet]
        public ActionResult<string> Index()
        {
            return "Use only post in [Find] method and form with [Cep.Value] parameter.";
        }

        [HttpPost]
        public ActionResult<Address> Find([FromBody]Cep cep)
        {
            return this.cepService.Get(cep.Value);
        }
    }
}
