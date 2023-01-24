using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMediaBackend.Identity;
using SocialMediaBackend.Models.View_Model;
using SocialMediaBackend.Services_Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaBackend.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationUserManager _usermanager;
        private readonly ApplicationSigninManager _signinmanager;
        private readonly AppSettingJWT _appSettingJwt;
        private object encode;

        public UserService(ApplicationUserManager usermanager,ApplicationSigninManager signinmanager,IOptions<AppSettingJWT> appsettingjwt)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _appSettingJwt = appsettingjwt.Value;
        }
        public async Task<ApplicationUser> Authenticate(LoginViewModel loginviewmodel)
        {
            if (loginviewmodel.Provider != null)
            {
            var findUser = await _usermanager.FindByNameAsync(loginviewmodel.Username);
                if(findUser == null)
                {
                    return null;
                }
            var userclaim = await _usermanager.GetLoginsAsync(findUser);
            var results = await _signinmanager.ExternalLoginSignInAsync(userclaim.FirstOrDefault().LoginProvider,userclaim.FirstOrDefault().ProviderKey,false);
                if (!results.Succeeded)
                {
                    return null;
                }
            }
            else
            {
            var result = await _signinmanager.PasswordSignInAsync(loginviewmodel.Username, loginviewmodel.Password, false, false);
                if (!result.Succeeded)
                    return null;
            }
                var applicationuser = await _usermanager.FindByNameAsync(loginviewmodel.Username);
                applicationuser.PasswordHash = "";
                // create jwt token on login
                if(await _usermanager.IsInRoleAsync(applicationuser, SD.roleAdmin))
                {
                    applicationuser.Role = SD.roleAdmin;
                }
                else
                {
                    applicationuser.Role = SD.roleUser;
                }
                var tokenhandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettingJwt.SecretKey);
                var tokendescritor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, applicationuser.Id.ToString()),
                        new Claim(ClaimTypes.Email, applicationuser.Email),
                        new Claim(ClaimTypes.Role, applicationuser.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenhandler.CreateToken(tokendescritor);
                applicationuser.Token = tokenhandler.WriteToken(token);
                return applicationuser;
           
        }

        public async Task<bool> RegisterUser(SignUpViewModel signupviewmodel)
        {
            var user = new ApplicationUser();
            user.UserName = signupviewmodel.userName.Replace(' ','_');
            user.Email = signupviewmodel.userEmail;
            var password = signupviewmodel.Password;
            if (signupviewmodel.provider != "")
            {
           var userCreate = await _usermanager.CreateAsync(user);
                if (userCreate.Succeeded)
                {
                    await _usermanager.AddToRoleAsync(user, SD.roleUser);
                    if (signupviewmodel.provider != null)
                    {
                    UserLoginInfo logininfo = new UserLoginInfo(signupviewmodel.provider, signupviewmodel.providerKey.ToString(), signupviewmodel.provider);
                    await _usermanager.AddLoginAsync(user,logininfo);
                    }
                    return true;
                }
                
                    return false;
                
            }
            else
            {
                var userCreate = await _usermanager.CreateAsync(user, password);
                if (userCreate.Succeeded)
                {
                    await _usermanager.AddToRoleAsync(user, SD.roleUser);
                    return true;
                }
               
                    return false;
                
            }
            
        }
    }
}
