using Microsoft.AspNetCore.Identity;
using System.TaskItem.API.ViewModel.Authenticate;

namespace TaskManagmentAPI.BusinessLogic.UserLogic.Interface
{
    public interface IUserManageService
    {
        Task<ValidateLoginViewModel> Authenticate(string username, string password);
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
        Task<bool> FindByEmailId(string? emailId);
        Task<IdentityUser> FindByNameAsync(string userName);
        Task<bool> CheckPassword(IdentityUser identityUser, string passWord);
    }
}
