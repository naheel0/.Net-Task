using Task3.Models;
namespace Task3.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
