using System.TaskItem.API.Model;
using System.TaskItem.API.Model.ApplicationModel;

namespace System.TaskItem.API.BusinessLogic.TaskLogic.Interface
{
    public interface ITaskManager
    {
        Task<SprintTaskViewModel> GetAllTaskAsync(TaskSearchModel taskSearchModel);
        Task<TaskViewModel?> GetTaskByIdAsync(int id);
        Task<bool?> UpdateSprintTaskAsync(TaskViewModel sprintTask);
        Task<bool?> SaveSprintTaskAsync(TaskViewModel sprintTask);
        Task<bool?> DeleteSprintTaskAsync(int id);
    }
}
