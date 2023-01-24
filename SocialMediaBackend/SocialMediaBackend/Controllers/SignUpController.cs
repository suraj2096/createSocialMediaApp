using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Identity;
using SocialMediaBackend.Models.View_Model;
using SocialMediaBackend.Services_Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaBackend.Controllers
{
    [Route("api/signup")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApplicationUserManager _usermanager;
        public SignUpController(IUserService userservice,ApplicationUserManager usermanager)
        {
            _userService = userservice;
            _usermanager = usermanager;
        }
        [HttpPost]
        // we use task because it define the returntype of async method 
        public async Task<IActionResult> Register([FromBody]SignUpViewModel signupviewmodel)
        {
            var user = await _userService.RegisterUser(signupviewmodel);
            if(user)
            {
                return Ok(new {success=true, message = "Register Successfully Please Login to Continue" });
            }
            if(signupviewmodel.userName.Contains(' '))
            {
                signupviewmodel.userName=signupviewmodel.userName.Replace(' ', '_');
            }
            var userExists = await _usermanager.FindByNameAsync(signupviewmodel.userName);
            if(userExists != null)
            {
                return Ok(new { success = false, message = "User Already Registered Please Login" });
            }
            return Ok(new { success=false,message = "You Enter the Wrong Ceredentials" });
        }
    }
}
