using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Models.View_Model;
using SocialMediaBackend.Services_Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaBackend.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userservice)
        {
            _userService = userservice;
        }
        [HttpPost]
        public async Task<ActionResult> Login([FromBody]LoginViewModel loginviewmodel)
        {
            if (loginviewmodel == null) return BadRequest();
            if (loginviewmodel.Username.Contains(' '))
            {
                loginviewmodel.Username = loginviewmodel.Username.Replace(' ', '_');
            }
            var user = await _userService.Authenticate(loginviewmodel);
            if(user == null)
            {
                return Ok(new { success=false,message = "Wrong Ceredentials Enter " });
            }
                return Ok(new { user, success=true,message = "Login Successfully" });
        }
    }
}
