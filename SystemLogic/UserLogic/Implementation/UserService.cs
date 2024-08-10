using Microsoft.AspNetCore.Identity;
using System.Data;
using TaskManagmentAPI.SystemLogic.UserSystemLogic.Interface;

namespace TaskManagmentAPI.SystemLogic.UserSystemLogic.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<bool> FindByEmailId(string? emailId)
        {
             bool isExist = false;
            if (emailId != null)
            {
                var result = await _userManager.FindByEmailAsync(emailId);
                if (result != null)
                {
                    isExist = true;
                }
            }

            return isExist;
        }
        public async Task<IdentityResult> AddUserToRole(IdentityUser user, string roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }
        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            if(result != null)
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
            return await _userManager.CheckPasswordAsync(identityUser, passWord);
        }

        public async Task<IList<string>> GetRoleAsync(IdentityUser identityUser)
        {
           return await _userManager.GetRolesAsync(identityUser);
        }
        public void getRoles()
        {
            IdentityRole role = new IdentityRole
            {
                Name = "Admin"
            };
            var rol = _roleManager.Roles;
        }
        public async void Updaterole()
        {
            IdentityRole role = new IdentityRole
            {
                
                NormalizedName = "ADMIN"
            };
           await _roleManager.UpdateNormalizedRoleNameAsync(role);
            string s = "me";
        }
    }
}
