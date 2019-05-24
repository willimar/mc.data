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
        private readonly CepService _cepService;

        public CepController(CepService cepService)
        {
            this._cepService = cepService;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public ActionResult<Address> Index(string cep)
        {
            var address = this._cepService.Get(cep);

            if (address == null || !address.IsValid())
            {
                return NotFound();
            }

            return address;
        }
    }
}
