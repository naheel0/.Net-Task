using Microsoft.AspNetCore.Mvc;
using Task3.Models;
using Task3.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private static List<User> _users = new();
    private readonly IJwtService _jwtService;

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
            Role = request.Role
        };

        _users.Add(user);

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var user = _users.FirstOrDefault(u =>
            u.Username == request.Username &&
            u.Password == request.Password);

        if (user == null)
            return Unauthorized("Invalid credentials");

        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }
}