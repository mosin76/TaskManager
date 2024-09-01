using Microsoft.EntityFrameworkCore;
using System.TaskItem.API.BusinessLogic.TaskLogic.Interface;
using System.TaskItem.API.Model;
using System.TaskItem.API.Model.ApplicationModel;
using TaskManagmentAPI.SystemLogic.TaskItem.Interface;

namespace System.TaskItem.API.BusinessLogic.TaskLogic.Implementation
{
    public class TaskManager : ITaskManager
    {
        private readonly ITaskItemService _taskItemService;
        public TaskManager(ITaskItemService taskItemService) 
        {
            _taskItemService = taskItemService;
        }
        public async Task<SprintTaskViewModel> GetAllTaskAsync(TaskSearchModel taskSearchModel)
        {
            return await _taskItemService.GetAllTaskAsync(taskSearchModel);
        }
        public async Task<TaskViewModel?> GetTaskByIdAsync(int id)
        {
            var sprinttask = await _taskItemService.GetTaskByIdAsync(id);
            return GetMappedTaskViewModel(sprinttask);
        }
        public async Task<bool?> UpdateSprintTaskAsync(TaskViewModel taskViewModel)
        {
            await _taskItemService.UpdateSprintTaskAsync(GetMappedTaskSprint(taskViewModel));
            return true;
        }
        public async Task<bool?> SaveSprintTaskAsync(TaskViewModel taskViewModel)
        {
            await _taskItemService.SaveSprintTaskAsync(GetMappedTaskSprint(taskViewModel));
            return true;
        }
        public async Task<bool?> DeleteSprintTaskAsync(int id)
        {
            await _taskItemService.DeleteSprintTaskAsync(id);
            return true;
            
        }
        private TaskViewModel GetMappedTaskViewModel(SprintTask? sprintTask)
        {
            TaskViewModel taskViewModel = new TaskViewModel();
            if (sprintTask != null)
            {
                taskViewModel.TaskId = sprintTask.TaskId;
                taskViewModel.Title= sprintTask.Title;
                taskViewModel.Status = sprintTask.Status;
                taskViewModel.UserId = sprintTask.UserId;
                taskViewModel.Description = sprintTask.Description;
                taskViewModel.StartDate = sprintTask.StartDate != null ? sprintTask.StartDate.Value.ToString("dd-MM-yyyy") : "";
                taskViewModel.EndDate= sprintTask.EndDate != null ? sprintTask.EndDate.Value.ToString("dd-MM-yyyy") : "";
                //taskViewModel.DueDate = sprintTask.DueDate != null ? sprintTask.DueDate.Value.ToString("MM-dd-yyyy") : "";
            }
            return taskViewModel;
        }
        private SprintTask GetMappedTaskSprint(TaskViewModel? taskViewModel)
        {
            SprintTask sprintTask = new SprintTask();
            var str = DateOnly.ParseExact(taskViewModel.StartDate, "dd-MM-yyyy", null);
            if (taskViewModel != null)
            {
                sprintTask.TaskId = taskViewModel.TaskId;
                sprintTask.Title = taskViewModel.Title;
                sprintTask.Status = taskViewModel.Status;
                sprintTask.UserId = taskViewModel.UserId;
                sprintTask.Description = taskViewModel.Description;
                sprintTask.StartDate = taskViewModel.StartDate != null ? DateOnly.ParseExact(taskViewModel.StartDate, "dd-MM-yyyy", null) : null;
                sprintTask.EndDate = taskViewModel.EndDate != null ? DateOnly.ParseExact(taskViewModel.EndDate, "dd-MM-yyyy", null) : null;
               // sprintTask.DueDate = taskViewModel.DueDate != null ? DateOnly.Parse(taskViewModel.DueDate) : null;
            }
            return sprintTask;
        }

    }
}
