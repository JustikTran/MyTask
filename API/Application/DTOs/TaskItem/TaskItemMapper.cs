using API.Application.DTOs.User;

namespace API.Application.DTOs.TaskItem
{
    public class TaskItemMapper
    {
        private static TaskItemMapper? _instance;
        private static readonly object _lock = new object();
        public static TaskItemMapper Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new TaskItemMapper();
                    }
                    return _instance;

                }
            }
        }
        public TaskItemResponse ToResponse(Domain.Entities.TaskItem? taskItem)
        {
            if (taskItem == null) return null!;
            return new TaskItemResponse
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                UserId = taskItem.UserId,
                Priority = taskItem.Priority.ToString(),
                Status = taskItem.Status.ToString(),
                StartDate = taskItem.StartDate,
                DueDate = taskItem.DueDate,
                CreateAt = taskItem.CreateAt,
                UpdateAt = taskItem.UpdateAt,
                User = taskItem.User != null ?
                UserMapper.Instance.ToResponse(taskItem.User) : null
            };
        }

        public Domain.Entities.TaskItem ToEntity(TaskItemCreateRequest request)
        {
            if (request == null) return null!;
            return new Domain.Entities.TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                UserId = Guid.Parse(request.UserId),
                Priority = request.Priority,
                Status = request.Status,
                StartDate = request.StartDate,
                DueDate = request.DueDate,
            };
        }
    }
}
