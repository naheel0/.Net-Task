using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> Employees = new List<Employee>
        {
             new Employee { Id = 1, Name = "John", Department = "IT",  },
             new Employee { Id = 2, Name = "Sara", Department = "HR",  }
        };
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            return Ok(Employees);
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            Employees.Add(employee);
            return Ok(employee);
        }
        [HttpPut("{Id}")]
        public IActionResult EditEmployee(int Id, Employee UpdatedEmployee)
        {
            var employee = Employees.FirstOrDefault(e => e.Id == Id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = UpdatedEmployee.Name;
            employee.Department = UpdatedEmployee.Department;
            return Ok(employee);
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteEmployee(int Id)
        {
            var employee = Employees.FirstOrDefault(e => e.Id == Id);
            if (employee == null)
            {
                return NotFound();
            }
            Employees.Remove(employee);
            return Ok(Employees);
        }
    }
}
