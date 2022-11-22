using JWTAuthenticationManager;
using JWTAuthenticationManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly JWTTokenHander _jWTTokenHander;

        public AccountController(JWTTokenHander jWTTokenHander)
        {
            this._jWTTokenHander = jWTTokenHander;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse = _jWTTokenHander.GenerateJWTToken(authenticationRequest);
            if (authenticationResponse == null) return Unauthorized();

            return authenticationResponse;
        }
    }
}
