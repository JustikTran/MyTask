using System.ComponentModel.DataAnnotations;

namespace API.Application.DTOs.Auth
{
    public class ChangePassword
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
