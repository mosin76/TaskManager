﻿using System.ComponentModel.DataAnnotations;

namespace System.TaskItem.API.Model.ApplicationModel
{
    public class SprintTask
    {
        [Key]
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? UserId { get; set; }
        public int Status { get; set; }
    }
}

