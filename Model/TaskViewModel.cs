using System.ComponentModel.DataAnnotations;

namespace System.TaskItem.API.Model
{
    public class TaskViewModel
    {
       
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; }
        //public string? DueDate { get; set; }
        public string? EndDate { get; set; }
        public string? UserId { get; set; }
        public int Status { get; set; }
    }
}
