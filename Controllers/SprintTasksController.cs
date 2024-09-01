using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.TaskItem.API.BusinessLogic.TaskLogic.Interface;
using System.TaskItem.API.Model;
using System.TaskItem.API.Model.ApplicationModel;
using Microsoft.AspNetCore.Cors;

namespace System.TaskItem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class SprintTasksController : ControllerBase
    {
        private readonly ITaskManager _context;

        public SprintTasksController(ITaskManager context)
        {
            _context = context;
        }

        [Route("task/gettaskbysearch")]
        [HttpGet]
        
        public async Task<ActionResult<SprintTaskViewModel>> GetSprintTask([Required][FromQuery]  TaskSearchModel taskSearchModel)
        {
            var userid = GetUserId();
            if (string.IsNullOrEmpty(userid))
            {
                return Unauthorized();
            }
            if (!string.IsNullOrEmpty(userid))
            {
                taskSearchModel.userId = userid;
            }
            return await _context.GetAllTaskAsync(taskSearchModel);
        }


        [Route("task/gettaskbyid/{id}")]
        [HttpGet]
        public async Task<ActionResult<TaskViewModel>> GetSprintTask(int id)
        {
            var userid = GetUserId();
            if (string.IsNullOrEmpty(userid))
            {
                return Unauthorized();
            }
            var sprintTask = await _context.GetTaskByIdAsync(id);

            if (sprintTask == null)
            {
                return NotFound();
            }

            return sprintTask;
        }
        [Route("task/updatetask/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutSprintTask(int id, TaskViewModel sprintTask)
        {
            var userid = GetUserId();
            if (id != sprintTask.TaskId)
            {
                return BadRequest();
            }
            if(string.IsNullOrEmpty(userid))
            {
                return Unauthorized();
            }
            if (!string.IsNullOrEmpty(userid))
            {
                sprintTask.UserId = userid;
                await _context.UpdateSprintTaskAsync(sprintTask);
            }
            return NoContent();
        }

        [Route("task/addtask")]
        [HttpPost]
        public async Task<ActionResult<SprintTask>> PostSprintTask(TaskViewModel sprintTask)
        {
            var userid = GetUserId();
            if (string.IsNullOrEmpty(userid))
            {
                return Unauthorized();
            }
            if (!string.IsNullOrEmpty(userid))
            {
                sprintTask.UserId = userid;
                await _context.SaveSprintTaskAsync(sprintTask);
            }
            
            return CreatedAtAction("GetSprintTask", new { id = sprintTask.TaskId }, sprintTask);
        }

        [Route("task/deletetask/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSprintTask(int id)
        {
            var userid = GetUserId();
            if (string.IsNullOrEmpty(userid))
            {
                return Unauthorized();
            }
            var isDelete = await _context.DeleteSprintTaskAsync(id);
            if (!isDelete.Value)
            {
                return NotFound();
            }
            return NoContent();
        }
        //getting userid from the claim
        private string GetUserId()
        {
            string useridclaim = string.Empty;
            var currentUser = HttpContext.User;
            var userid = currentUser.Claims.Where(e => e.Type == "userid");
            if (userid != null && userid.Count() > 0)
            {
                useridclaim= userid.FirstOrDefault().Value;
            }
            return useridclaim;
        }
        
    }
}
