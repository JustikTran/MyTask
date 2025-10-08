using System.ComponentModel.DataAnnotations;

namespace API.Application.DTOs.Auth
{
    public class RegisterRequest
    {
        /// <example>admin1</example>
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Username { get; set; } = string.Empty;

        /// <example>Admin@123</example>
        [Required]
        [StringLength(32, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        ///<example>admin@example.com</example>
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <example>John</example>
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        /// <example>Doe</example>
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;
    }
}
