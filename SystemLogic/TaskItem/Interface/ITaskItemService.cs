using System.TaskItem.API.Model.ApplicationModel;

namespace TaskManagmentAPI.SystemLogic.TaskItem.Interface
{
    public interface  ITaskItemService
    {
        Task<List<SprintTask>> GetAllTaskAsync();
        Task<SprintTask?> GetTaskByIdAsync(int id);
        Task<bool?> UpdateSprintTaskAsync(SprintTask sprintTask);
        Task<bool?> SaveSprintTaskAsync(SprintTask sprintTask);
        Task<bool?> DeleteSprintTaskAsync(int id);
    }
}
