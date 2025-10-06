using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.Application.DTOs.TaskItem
{
    public class TaskItemCreateRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [AllowNull]
        public string? Description { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [RegularExpression("Low|Medium|High", ErrorMessage = "Priority must be Low, Medium, or High.")]
        public string Priority { get; set; } = string.Empty;

        [Required]
        [RegularExpression("ToDo|InProgress|Overdue|Completed", ErrorMessage = "Status must be ToDo, InProgress, Overdue, or Completed.")]
        public string Status { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
    }
}
