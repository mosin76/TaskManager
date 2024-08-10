using System.ComponentModel.DataAnnotations;

namespace System.TaskItem.API.Model.UserModel.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "UserName is Required.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is Required.")]
        public string? Password { get; set; }
    }
}
