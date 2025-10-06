using API.Application.DTOs.TaskItem;
using API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace API.Controllers
{
    [Route("api/task-item")]
    [ApiController]
    public class TaskItemController : ODataController
    {
        private readonly ITaskItemRepository taskItemRepository;
        public TaskItemController(ITaskItemRepository taskItemRepository)
        {
            this.taskItemRepository = taskItemRepository;
        }

        [HttpGet("{userId}")]
        [EnableQuery]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<TaskItemResponse>> GetTaskItemsByUserId([FromRoute] string userId)
        {
            var taskItems = taskItemRepository.GetTaskItems(userId);
            return Ok(taskItems.AsQueryable());
        }

        [HttpGet("id={id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTaskItemById([FromRoute] string id)
        {
            var taskItem = await taskItemRepository.GetTaskItemById(id);
            if (taskItem == null)
            {
                return NotFound();
            }
            return Ok(taskItem);
        }

        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateTaskItem([FromBody] TaskItemCreateRequest request)
        {
            var createdTaskItem = await taskItemRepository.CreateTaskItem(request);
            return StatusCode(createdTaskItem.StatusCode, createdTaskItem);
        }

        [HttpPut("update/id={id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateTaskItem([FromRoute] string id, [FromBody] TaskItemUpdateRequest request)
        {
            var updatedTaskItem = await taskItemRepository.UpdateTaskItem(request);
            return StatusCode(updatedTaskItem.StatusCode, updatedTaskItem);
        }

        [HttpDelete("delete/id={id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteTaskItem([FromRoute] string id)
        {
            var deletedTaskItem = await taskItemRepository.DeleteTaskItem(id);
            return StatusCode(deletedTaskItem.StatusCode, deletedTaskItem);
        }
    }
}
