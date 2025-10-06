using API.Application.DTOs.User;

namespace API.Application.DTOs.TaskItem
{
    public class TaskItemResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid UserId { get; set; }
        public UserResponse? User { get; set; }
        public string? Priority { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
