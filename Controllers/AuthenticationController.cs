using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.TaskItem.API.Model.UserModel.Login;
using System.TaskItem.API.Model.UserModel.SignUp;
using TaskManagmentAPI.BusinessLogic.UserLogic.Interface;
using TaskManagmentAPI.SystemLogic.UserSystemLogic.Interface;
using System.TaskItem.API.Model.UserModel;
using System.TaskItem.API.Common;

namespace System.TaskItem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                    return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse { Message = TaskConstant.UserAlready, Status = TaskConstant.ErrorMessage });
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
                        return StatusCode(StatusCodes.Status201Created, new ApiResponse { Message = TaskConstant.UserCreatedSuccess, Status = TaskConstant.SuccessMessage });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Message = TaskConstant.UserCreationFailed, Status = TaskConstant.ErrorMessage });
                    }
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse { Message = TaskConstant.UserEmailNotEmpty, Status = TaskConstant.ErrorMessage });
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
