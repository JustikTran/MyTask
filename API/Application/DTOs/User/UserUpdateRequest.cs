using System.ComponentModel.DataAnnotations;

namespace API.Application.DTOs.User
{
    public class UserUpdateRequest
    {
        /// <example>550e8400-e29b-41d4-a716-446655440000</example>
        [Required]
        public string Id { get; set; } = string.Empty;

        /// <example>admin1</example>
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Username { get; set; } = string.Empty;

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

        /// <example>true</example>
        public bool IsActive { get; set; }
    }
}
