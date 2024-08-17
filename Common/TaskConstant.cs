namespace System.TaskItem.API.Common
{
    public static class TaskConstant
    {
        public const string UserAlready = "User Already Exist!";
        public const string ErrorMessage = "Error";
        public const string SuccessMessage = "Success";
        public const string UserCreatedSuccess = "User Created Successfully";
        public const string UserCreationFailed = "User Failed to Create";
        public const string UserEmailNotEmpty = "Email Can Not be Empty!";
        public const string SearchTitle = "title";
        public const string SearchDesciption = "description";
        public const string SearchStatus = "status";
        public const string OrderByDesc = "desc";
        public const string OrderByAsc = "asc";
    }
    public enum SprintTaskStatus { ToDo = 1, InProgress, Done }

}
