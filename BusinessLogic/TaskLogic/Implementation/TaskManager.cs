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
        public async Task<SprintTask?> GetTaskByIdAsync(int id)
        {
            return await _taskItemService.GetTaskByIdAsync(id);
        }
        public async Task<bool?> UpdateSprintTaskAsync(SprintTask sprintTask)
        {
            await _taskItemService.UpdateSprintTaskAsync(sprintTask);
            return true;
        }
        public async Task<bool?> SaveSprintTaskAsync(SprintTask sprintTask)
        {
            await _taskItemService.SaveSprintTaskAsync(sprintTask);
            return true;
        }
        public async Task<bool?> DeleteSprintTaskAsync(int id)
        {
            await _taskItemService.DeleteSprintTaskAsync(id);
            return true;
            
        }

    }
}
