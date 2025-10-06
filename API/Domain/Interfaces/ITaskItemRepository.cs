using API.Application.DTOs;
using API.Application.DTOs.TaskItem;
using API.Domain.Entities;

namespace API.Domain.Interfaces
{
    public interface ITaskItemRepository
    {
        IQueryable<TaskItemResponse> GetTaskItems(string userId);
        Task<TaskItemResponse?> GetTaskItemById(string id);
        Task<Response> CreateTaskItem(TaskItemCreateRequest taskItem);
        Task<Response> UpdateTaskItem(TaskItemUpdateRequest taskItem);
        Task<Response> DeleteTaskItem(string id);
    }
}
