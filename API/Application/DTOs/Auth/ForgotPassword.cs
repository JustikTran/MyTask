using System.ComponentModel.DataAnnotations;

namespace API.Application.DTOs.Auth
{
    public class ForgotPassword
    {
        /// <example>admin1</example>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <example>NewPassword@123</example>
        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
