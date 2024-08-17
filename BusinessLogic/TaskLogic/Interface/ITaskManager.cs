using System.TaskItem.API.Model;
using System.TaskItem.API.Model.ApplicationModel;

namespace System.TaskItem.API.BusinessLogic.TaskLogic.Interface
{
    public interface ITaskManager
    {
        Task<SprintTaskViewModel> GetAllTaskAsync(TaskSearchModel taskSearchModel);
        Task<SprintTask?> GetTaskByIdAsync(int id);
        Task<bool?> UpdateSprintTaskAsync(SprintTask sprintTask);
        Task<bool?> SaveSprintTaskAsync(SprintTask sprintTask);
        Task<bool?> DeleteSprintTaskAsync(int id);
    }
}
