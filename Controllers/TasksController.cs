
using AspnetCoreSqliteApi.Models;
using AspnetCoreSqliteApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSqliteApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // GET: api/Tasks
    [HttpGet]
    public ActionResult<ServiceResponse<IEnumerable<TaskModel>>> GetTasks() 
    {
        return _taskService.GetTasks();
    }

    // GET: api/Tasks/1
    [HttpGet("{id}")]
    public ActionResult<ServiceResponse<TaskModel>> GetTask(int id) 
    {
        var res = _taskService.GetTask(id);
        if (res.Data == null)
        {
            return NotFound();
        }
        return res;
    }

    // POST: api/Tasks
    [HttpPost]
    public ActionResult<ServiceResponse<TaskModel>> AddTask(TaskModel task) {
        _taskService.AddTask(task);

        return CreatedAtAction(nameof(GetTask), task);
    }

    // PUT: api/Tasks/2
    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, TaskModel task) {
        if (id != task.Id)
        {
            return BadRequest();
        }

        var res = _taskService.GetTask(task.Id);
        if (res.Data == null)
        {
            return NotFound();
        }
        _taskService.UpdateTask(task);
         return NoContent();
    }

    // DELETE: api/Tasks/2
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id) {
        var res = _taskService.GetTask(id);
        if (res.Data == null)
        {
            return NotFound();
        }
        _taskService.DeleteTask(res.Data);
        return NoContent();
    }
}