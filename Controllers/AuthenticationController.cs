using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.TaskItem.API.Model.UserModel.Login;
using System.TaskItem.API.Model.UserModel.SignUp;
using TaskManagmentAPI.BusinessLogic.UserLogic.Interface;
using System.TaskItem.API.Model.UserModel;
using System.TaskItem.API.Common;
using Microsoft.AspNetCore.Cors;
namespace System.TaskItem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ValidateAntiForgeryToken]
    [EnableCors("MyCorsPolicy")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserManageService _userManageService;
        public AuthenticationController(
            IUserManageService userManageService
            )
        {
            _userManageService = userManageService;
        }
        [Route("task/register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([Required][FromBody] RegistrationModel registerUser)
        {
            if (registerUser.Email != null && registerUser.Password != null)
            {
                bool isUserExist = await _userManageService.FindByEmailId(registerUser.Email);
                if (isUserExist)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse { Message = TaskConstant.UserAlready, IsSuccess = false });
                }
                else
                {
                    IdentityUser user = new()
                    {
                        Email = registerUser.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = registerUser.UserName,

                    };
                    var result = await _userManageService.CreateUserAsync(user, registerUser.Password);
                    if (result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status201Created, new ApiResponse { Message = TaskConstant.UserCreatedSuccess, IsSuccess = true });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Message = TaskConstant.UserCreationFailed, IsSuccess = false });
                    }
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse { Message = TaskConstant.UserEmailNotEmpty, IsSuccess = false });
            }


        }
        [Route("task/login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([Required][FromBody] LoginModel loginModel)
        {

            if (loginModel == null || loginModel.UserName == null || loginModel.Password == null)
                return Unauthorized();
            var token = await _userManageService.Authenticate(loginModel.UserName, loginModel.Password);
            if (token != null && !token.IsValidationSuccess)
            {
                return Unauthorized(token);
            }
            return Ok(token);
        }
    }
}
