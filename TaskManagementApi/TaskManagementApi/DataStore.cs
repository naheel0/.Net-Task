using TaskManagementApi.Models;

namespace TaskManagementApi
{
    public class DataStore
    {
        public List<User> Users { get; set; } = new();
        public List<TodoTask> Tasks { get; set; } = new();
    }
}
