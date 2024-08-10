namespace System.TaskItem.API.ViewModel.Authenticate
{
    public class ValidateLoginViewModel
    {
        public bool IsValidationSuccess { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
    }
}
