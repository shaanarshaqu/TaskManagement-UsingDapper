using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data.DTO;
using TaskManagement.Data.Models;
using TaskManagement.Manager.Interface;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskManager taskManager;
        public TaskController(ITaskManager taskManager)
        {
            this.taskManager = taskManager;
        }


        [HttpGet("All/{userid}")]
        public async Task<IActionResult> GetAllTasks(string userid)
        {
            try
            {
                IEnumerable<Tasks> AllTasks = await taskManager.GetAllTask(userid);
                return Ok(AllTasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(string id)
        {
            try
            {
                Tasks TaskById = await taskManager.GetTaskById(id);
                return TaskById != null ? Ok(TaskById) : NotFound("Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPost("create-task")]
        public async Task<IActionResult> CreateNewTask([FromBody] TasksDto tasks)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                bool IsCreated = await taskManager.AddNewTask(tasks);
                return IsCreated ? Ok(IsCreated) : BadRequest(IsCreated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-task")]
        public async Task<IActionResult> UpdateTask([FromBody]Tasks tasks)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                bool IsUpdated = await taskManager.UpdateTask(tasks);
                return IsUpdated ? Ok(IsUpdated) : BadRequest(IsUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            try
            {
                bool IsDeleted = await taskManager.DeleteTask(id);
                return IsDeleted ? Ok(IsDeleted) : BadRequest(IsDeleted);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
