using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.Application.DTOs.TaskItem
{
    public class TaskItemCreateRequest
    {
        /// <example>Complete project documentation</example>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        /// <example>Finish writing the documentation for the new project by end of the week.</example>
        [AllowNull]
        public string? Description { get; set; }

        /// <example>550e8400-e29b-41d4-a716-446655440000</example>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <example>High</example>
        [Required]
        [RegularExpression("Low|Medium|High", ErrorMessage = "Priority must be Low, Medium, or High.")]
        public string Priority { get; set; } = string.Empty;

        /// <example>ToDo</example>
        [Required]
        [RegularExpression("ToDo|InProgress|Overdue|Completed", ErrorMessage = "Status must be ToDo, InProgress, Overdue, or Completed.")]
        public string Status { get; set; } = string.Empty;

        /// <example>2025-10-05T09:00:00Z</example>
        [Required]
        public DateTime StartDate { get; set; }
        /// <example>2025-10-10T17:00:00Z</example>
        [Required]
        public DateTime DueDate { get; set; }
    }
}
