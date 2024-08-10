using System.ComponentModel.DataAnnotations;

namespace System.TaskItem.API.Model.ApplicationModel
{
    public class SprintTask
    {
        [Key]
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? ProfileImage { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
    }
}

