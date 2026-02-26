using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task4.Models;
using task4.Services;

namespace task4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _service;

        public ItemsController(IItemService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var items = _service.GetAll();
                return Ok(items);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var item = _service.GetById(id);
                if (item == null) return NotFound();
                return Ok(item);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] Item item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.Title))
                    return BadRequest("Title cannot be empty");

                var created = _service.Add(item);
                return CreatedAtAction(nameof(Get), new { id = created.id }, created);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Item item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.Title))
                    return BadRequest("Title cannot be empty");

                var updated = _service.Update(id, item);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
