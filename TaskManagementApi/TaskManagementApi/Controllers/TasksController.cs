using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly DataStore _store;
        public TasksController(DataStore store)
        {
            _store = store;
        }
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create(TodoTask task)
        {
            task.Id = _store.Tasks.Count + 1;
            _store.Tasks.Add(task);
            return Ok(task);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {
            return Ok(_store.Tasks);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Update(int id, TodoTask updated)
        {
            var task =_store.Tasks.FirstOrDefault(t=> t.Id == id);
            if (task == null) return NotFound("Task not found");
            task.Title = updated.Title;
            task.Description = updated.Description;
            task.Status = updated.Status;
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var task = _store.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            _store.Tasks.Remove(task);  
            return NoContent();
        }

    }
}
