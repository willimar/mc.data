using mc.api.user.Services;
using mc.core.domain.register.Entity.Person;
using mc.user.service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mc.api.user.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public ActionResult<User> PostUser([FromBody]User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Password = mc.cript.Cryptographer.Encrypt(user.Password, Consts.PASS_PHRASE);

            return this.userService.AppenData(user);
        } 
    }
}
