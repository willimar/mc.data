using mc.api.user.Model;
using mc.api.user.Services;
using mc.core.domain.register.Entity.Person;
using mc.cript;
using mc.user.service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace mc.api.user.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public LoginController(IUserService userService, 
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            this._userService = userService;
            this._signingConfigurations = signingConfigurations;
            this._tokenConfigurations = tokenConfigurations;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody]UserModel user)
        {
            bool isValidCredentials = false;
            if (user != null && !string.IsNullOrWhiteSpace(user.UserName))
            {
                //var password = mc.cript.Cryptographer.Encrypt(user.Password, Consts.PASS_PHRASE);

                var users = this._userService.GetData(u => u.UserName.Equals(user.UserName));

                isValidCredentials = users.Any(u => mc.cript.Cryptographer.Decrypt(u.Password, Consts.PASS_PHRASE).Equals(user.Password));
            }

            if (isValidCredentials)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.UserName, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                    }
                );

                DateTime date = DateTime.Now;
                DateTime expireDate = date +
                    TimeSpan.FromSeconds(this._tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = this._tokenConfigurations.Issuer,
                    Audience = this._tokenConfigurations.Audience,
                    SigningCredentials = this._signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = date,
                    Expires = expireDate
                });

                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = date.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = expireDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Autenticate fail."
                };
            }
        }
    }
}
