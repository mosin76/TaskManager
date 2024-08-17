namespace System.TaskItem.API.Model
{
    public class TaskSearchModel
    {
        public string? SearchFor { get; set; }
        public string? SearchValue { get; set; }
        public string? Status {  get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public string? userId {  get; set; }
        public int? PageNo { get; set; }
        public int? PazeSize { get; set; }
        public int? TotalRecord { get; set; }
    }
}
