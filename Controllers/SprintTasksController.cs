using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.TaskItem.API.BusinessLogic.TaskLogic.Interface;
using System.TaskItem.API.Model;
using System.TaskItem.API.Model.ApplicationModel;

namespace System.TaskItem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [ValidateAntiForgeryToken]
    public class SprintTasksController : ControllerBase
    {
        private readonly ITaskManager _context;

        public SprintTasksController(ITaskManager context)
        {
            _context = context;
        }

        [Route("task/GetTaskBySearch")]
        [HttpPost]
        
        public async Task<ActionResult<SprintTaskViewModel>> GetSprintTask([Required][FromBody] TaskSearchModel taskSearchModel)
        {
            return await _context.GetAllTaskAsync(taskSearchModel);
        }

        // GET: api/SprintTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskViewModel>> GetSprintTask(int id)
        {
            var sprintTask = await _context.GetTaskByIdAsync(id);

            if (sprintTask == null)
            {
                return NotFound();
            }

            return sprintTask;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSprintTask(int id, TaskViewModel sprintTask)
        {
            if (id != sprintTask.TaskId)
            {
                return BadRequest();
            }
            
                await _context.UpdateSprintTaskAsync(sprintTask);
                return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<SprintTask>> PostSprintTask(TaskViewModel sprintTask)
        {
            var currentUser = HttpContext.User;
            var userid = currentUser.Claims.Where(e => e.Type == "userid");
            if (userid!= null && userid.Count()>0)
            {

                sprintTask.UserId = userid.FirstOrDefault().Value;
                await _context.SaveSprintTaskAsync(sprintTask);
            }
            
            return CreatedAtAction("GetSprintTask", new { id = sprintTask.TaskId }, sprintTask);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSprintTask(int id)
        {
            var isDelete = await _context.DeleteSprintTaskAsync(id);
            if (!isDelete.Value)
            {
                return NotFound();
            }
            return NoContent();
        }
        
    }
}
