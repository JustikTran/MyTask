using System.ComponentModel.DataAnnotations;

namespace API.Application.DTOs.Auth
{
    public class ChangePassword
    {
        /// <example>550e8400-e29b-41d4-a716-446655440000</example>
        [Required]
        public string Id { get; set; } = string.Empty;
        /// <example>OldPassword@123</example>
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;
        /// <example>NewPassword@123</example>
        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
