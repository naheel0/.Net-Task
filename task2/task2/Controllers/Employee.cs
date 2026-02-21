using Microsoft.AspNetCore.Mvc;
using task2.Models;
using task2.Services;

namespace task2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            var added = _employeeService.Add(employee);
            return Ok(added);
        }

        [HttpPut("{id}")]
        public IActionResult EditEmployee(int id, Employee employee)
        {
            var updated = _employeeService.Update(id, employee);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var success = _employeeService.Delete(id);
            if (!success) return NotFound();
            return Ok(_employeeService.GetAllEmployees());
        }
    }
}