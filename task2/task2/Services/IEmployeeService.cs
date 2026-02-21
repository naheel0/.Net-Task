using task2.Models;

namespace task2.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Models.Employee> GetAllEmployees();
        Employee? GetById(int id);
        Employee Add(Employee employee);
        Employee Update( int id,Employee employee);
        bool Delete(int id);
    }
}
