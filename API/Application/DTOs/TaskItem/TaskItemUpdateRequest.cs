using System.ComponentModel.DataAnnotations;

namespace API.Application.DTOs.TaskItem
{
    public class TaskItemUpdateRequest : TaskItemCreateRequest
    {
        [Required]
        public string Id { get; set; } = string.Empty;
    }
}
