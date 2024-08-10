using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.TaskItem.API.Model;
using System.TaskItem.API.Model.ApplicationModel;
using TaskManagmentAPI.SystemLogic.TaskItem.Interface;

namespace TaskManagmentAPI.SystemLogic.TaskItem.Implementation
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ApplicationDbContext _context;
        public TaskItemService(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<List<SprintTask>> GetAllTaskAsync()
        {
           return await _context.SprintTask.ToListAsync();
            
        }
        public async Task<SprintTask?> GetTaskByIdAsync(int id)
        {
            return await _context.SprintTask.FindAsync(id);
        }
        public async Task<bool?> UpdateSprintTaskAsync(SprintTask sprintTask)
        {
            _context.Entry(sprintTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool?> SaveSprintTaskAsync(SprintTask sprintTask)
        {
            _context.SprintTask.Add(sprintTask);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool?> DeleteSprintTaskAsync(int id)
        {
            var sprintTask = await _context.SprintTask.FindAsync(id);
            if (sprintTask == null)
            {
                return false;
            }
            _context.SprintTask.Remove(sprintTask);
            await _context.SaveChangesAsync();
            return true;
        }
        private bool SprintTaskExist(int id)
        {
            return _context.SprintTask.Any(e => e.TaskId == id);
        }

    }
}
