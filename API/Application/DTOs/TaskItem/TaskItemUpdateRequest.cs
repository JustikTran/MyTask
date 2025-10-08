using System.ComponentModel.DataAnnotations;

namespace API.Application.DTOs.TaskItem
{
    public class TaskItemUpdateRequest : TaskItemCreateRequest
    {
        /// <example>550e8400-e29b-41d4-a716-446655440000</example>
        [Required]
        public string Id { get; set; } = string.Empty;
    }
}
