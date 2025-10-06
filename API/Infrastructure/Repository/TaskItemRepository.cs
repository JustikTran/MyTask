using API.Application.DTOs;
using API.Application.DTOs.TaskItem;
using API.Domain.Entities;
using API.Domain.Interfaces;
using API.Infrastructure.Data;

namespace API.Infrastructure.Repository
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly AppDbContext context;
        public TaskItemRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Response> CreateTaskItem(TaskItemCreateRequest taskItem)
        {
            try
            {
                var task = TaskItemMapper.Instance.ToEntity(taskItem);
                context.TaskItems.Add(task);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Create task item successfully"
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message.ToString()
                };
            }
        }

        public async Task<Response> DeleteTaskItem(string id)
        {
            try
            {
                var existing = await context.TaskItems.FindAsync(Guid.Parse(id));
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Task item not found"
                    };
                }
                context.TaskItems.Remove(existing);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Delete task item successfully"
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message.ToString()
                };
            }
        }

        public async Task<TaskItemResponse?> GetTaskItemById(string id)
        {
            try
            {
                var existing = await context.TaskItems.FindAsync(Guid.Parse(id));
                return TaskItemMapper.Instance.ToResponse(existing);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message.ToString());
            }
        }

        public IQueryable<TaskItemResponse> GetTaskItems(string userId)
        {
            try
            {
                var listTasks = context.TaskItems.Where(t => t.UserId == Guid.Parse(userId)).ToList();
                return listTasks
                    .Select(TaskItemMapper.Instance.ToResponse)
                    .AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message.ToString());
            }
        }

        public async Task<Response> UpdateTaskItem(TaskItemUpdateRequest taskItem)
        {
            try
            {
                var existing = await context.TaskItems.FindAsync(Guid.Parse(taskItem.Id));
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Task item not found"
                    };
                }
                existing.Title = taskItem.Title;
                existing.Description = taskItem.Description;
                existing.Priority = taskItem.Priority;
                existing.Status = taskItem.Status;
                existing.StartDate = taskItem.StartDate;
                existing.DueDate = taskItem.DueDate;
                existing.UpdateAt = DateTime.UtcNow;
                context.TaskItems.Update(existing);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Update task item successfully"
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message.ToString()
                };  
            }
        }
    }
}
