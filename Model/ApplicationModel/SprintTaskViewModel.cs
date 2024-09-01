namespace System.TaskItem.API.Model.ApplicationModel
{
    public class SprintTaskViewModel
    {
        public IList<SprintTask>? sprintTasks { get; set; }
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }
        public int? Total { get; set; }
        public bool? Success { get; set; }
        
    }
}
