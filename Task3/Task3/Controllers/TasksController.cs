using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Task3.Models;

namespace Task3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private static List<TodoTask> _tasks = new();
        private static int _id = 1;
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tasks);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(TodoTask task)
        {
            task.Id = _id++;
            _tasks.Add(task);
            return Ok(task);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, TodoTask updatedTask)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            task.Title = updatedTask.Title;
            return Ok(task);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            _tasks.Remove(task);
            return Ok("Task deleted successfully");
        }

    }
}