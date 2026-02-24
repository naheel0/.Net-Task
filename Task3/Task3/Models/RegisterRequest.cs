using System.ComponentModel.DataAnnotations;
namespace Task3.Models
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }=string.Empty;
        [Required]
        public string Password { get; set; }=string.Empty;
        [Required]
        public string Role { get; set; }=string.Empty;
    }
}
