using Microsoft.AspNetCore.Identity;

namespace TaskManagmentAPI.SystemLogic.UserSystemLogic.Interface
{
    public interface IUserService
    {
        Task<bool> FindByEmailId(string emailId);
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
        Task<IdentityResult> AddUserToRole(IdentityUser user, string roleName);
        Task<IdentityUser> FindByNameAsync(string userName);
        Task<bool> CheckPassword(IdentityUser identityUser, string passWord);
        Task<IList<string>> GetRoleAsync(IdentityUser identityUser);
    }
}
