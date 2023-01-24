using SocialMediaBackend.Identity;
using SocialMediaBackend.Models.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaBackend.Services_Contract
{
    public interface IUserService
    {
        public Task<ApplicationUser> Authenticate(LoginViewModel loginviewmodel);
        public Task<bool> RegisterUser(SignUpViewModel signupviewmodel);
    }
}
