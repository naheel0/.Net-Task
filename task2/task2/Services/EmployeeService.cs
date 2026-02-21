using task2.Models;
using System.Collections.Generic;
using System.Linq;

namespace task2.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees;

        public EmployeeService()
        {
            _employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Department = "HR" },
                new Employee { Id = 2, Name = "Jane Smith", Department = "IT" }
            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        // <-- Corrected casing to match interface
        public Employee? GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public Employee Add(Employee employee)
        {
            _employees.Add(employee);
            return employee;
        }

        public Employee? Update(int id, Employee employee)
        {
            var existing = _employees.FirstOrDefault(e => e.Id == id);
            if (existing == null) return null;

            existing.Name = employee.Name;
            existing.Department = employee.Department;
            return existing;
        }

        public bool Delete(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return false;

            _employees.Remove(employee);
            return true;
        }
    }
}