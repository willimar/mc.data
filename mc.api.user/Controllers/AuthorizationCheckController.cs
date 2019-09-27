using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mc.api.user.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizationCheckController : Controller
    {
        [Authorize("Bearer")]
        [HttpGet]
        public object Get()
        {
            return new
            {
                authorised = true
            };
        }
    }
}
