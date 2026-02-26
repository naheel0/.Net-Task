using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataStore _store;
        private readonly string key = "THIS_IS_MY_SUPER_SECRET_KEY_12345";
        public AuthController(DataStore store)
        {
            _store = store;
        }
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            _store.Users.Add(user);
            return Ok("User registered successfully");
        }
        [HttpPost("login")]
        public IActionResult Login(User login)
        {
            var user = _store.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
            if (user == null) 
                return Unauthorized("Invalid credentials");
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var keyBytes=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds=new SigningCredentials(keyBytes,SecurityAlgorithms.HmacSha256);
            var token=new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:creds
            );
            var jwtToken=new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = jwtToken });
        }
    }
}
