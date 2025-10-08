using System.ComponentModel.DataAnnotations;

namespace API.Application.DTOs.Auth
{
    public class LoginRequest
    {
        ///<example>admin1</example>
        [Required]
        public string Username { get; set; } = string.Empty;

        ///<example>Admin@123</example>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
