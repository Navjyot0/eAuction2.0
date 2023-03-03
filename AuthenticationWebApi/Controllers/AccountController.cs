using AuthenticationWebApi.Models;
using JWTAuthenticationManager;
using JWTAuthenticationManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> _userManager;
        private readonly JWTTokenHander _jWTTokenHander;

        public AccountController(UserManager<ApplicationUser> userManager, JWTTokenHander jWTTokenHander)
        {
            this._userManager = userManager;
            this._jWTTokenHander = jWTTokenHander;
        }

        // POST api/<UserController>
        //[AllowAnonymous]
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = user.Email,
                    Email = user.Email
                };

                IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);
                if (result.Succeeded)
                    return StatusCode(StatusCodes.Status201Created);
                return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }



        [HttpPost]
        public ActionResult<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest)
        {
            var account = _userManager.Users.Where(u => u.UserName == authenticationRequest.UserName).FirstOrDefault(); // && u.PasswordHash == authenticationRequest.Password).FirstOrDefault();

            UserAccount userAccount = account != null ? new UserAccount() { UserName = account?.UserName, Role = "" } : null;

            var authenticationResponse = _jWTTokenHander.GenerateJWTToken(userAccount);
            if (authenticationResponse == null) return Unauthorized();

            return authenticationResponse;
        }
    }
}
