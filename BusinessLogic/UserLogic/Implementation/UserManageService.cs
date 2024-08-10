using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.TaskItem.API.ViewModel.Authenticate;
using System.Text;
using TaskManagmentAPI.BusinessLogic.UserLogic.Interface;
using TaskManagmentAPI.SystemLogic.UserSystemLogic.Interface;

namespace TaskManagmentAPI.BusinessLogic.UserLogic.Implementation
{
    public class UserManageService : IUserManageService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserManageService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        public async Task<ValidateLoginViewModel> Authenticate(string username, string password)
        {
            ValidateLoginViewModel validateLoginViewModel = null;
            var UserExist = await _userService.FindByNameAsync(username);
            if (UserExist == null)
            {
                validateLoginViewModel=new ValidateLoginViewModel { IsValidationSuccess = false, Message= "User Does not exists!",Token="" };
                return validateLoginViewModel;
            }
            if (UserExist != null && await _userService.CheckPassword(UserExist, password))
            {
                validateLoginViewModel = new ValidateLoginViewModel { IsValidationSuccess = true, Message = "Login Successfull!", Token = GetToken(UserExist) };
                return validateLoginViewModel;
            }
            else
            {
                validateLoginViewModel = new ValidateLoginViewModel { IsValidationSuccess = false, Message = "User and Password Does not Match!", Token = "" };
                return validateLoginViewModel;
            }
            
        }
        private string GetToken(IdentityUser? user)
        {
            string jwttoken = string.Empty;
            if (user != null && !string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(_configuration["Jwt:Key"]))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Sid, user.Id),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                jwttoken = tokenHandler.WriteToken(token);
            }
            return jwttoken;
        }
        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
        {
            return await _userService.CreateUserAsync(user, password);
        }
        public async Task<bool> FindByEmailId(string? emailId)
        {
            bool isExist = false;
            if (emailId != null)
            {
                isExist = await _userService.FindByEmailId(emailId);
                          
            }

            return isExist;
        }
        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            var result = await _userService.FindByNameAsync(userName);
            if (result != null)
            {
                return result;
            }
            else
            {
                return new IdentityUser();
            }
        }
        public async Task<bool> CheckPassword(IdentityUser identityUser, string passWord)
        {
            return await _userService.CheckPassword(identityUser, passWord);
        }

    }
}
