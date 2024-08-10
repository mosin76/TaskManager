using System.TaskItem.API.Model.ApplicationModel;

namespace System.TaskItem.API.BusinessLogic.TaskLogic.Interface
{
    public interface ITaskManager
    {
        Task<List<SprintTask>> GetAllTaskAsync();
        Task<SprintTask?> GetTaskByIdAsync(int id);
        Task<bool?> UpdateSprintTaskAsync(SprintTask sprintTask);
        Task<bool?> SaveSprintTaskAsync(SprintTask sprintTask);
        Task<bool?> DeleteSprintTaskAsync(int id);
    }
}
