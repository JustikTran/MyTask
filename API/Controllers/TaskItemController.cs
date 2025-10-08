using API.Application.DTOs;
using API.Application.DTOs.TaskItem;
using API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("odata/task-item")]
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
        [SwaggerOperation(
            Summary = "Get all task items for a user",
            Description = "Retrieve a list of all task items associated with a specific user.")]
        [SwaggerResponse(200, "List of task items retrieved successfully", typeof(IEnumerable<TaskItemResponse>))]
        [SwaggerResponse(401, "Unauthorized access")]
        public ActionResult<IEnumerable<TaskItemResponse>> GetTaskItemsByUserId([FromRoute] string userId)
        {
            var taskItems = taskItemRepository.GetTaskItems(userId);
            return Ok(taskItems.AsQueryable());
        }

        [HttpGet("id={id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Get task item by ID",
            Description = "Retrieve a task item by its unique ID. Authorization required.")]
        [SwaggerResponse(200, "Task item found", typeof(TaskItemResponse))]
        [SwaggerResponse(401, "Unauthorized access")]
        [SwaggerResponse(404, "Task item not found")]
        [SwaggerResponse(500, "Internal server error")]
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
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Create a new task item",
            Description = "Create a new task item with the provided details. Authorization required.")]
        [SwaggerResponse(201, "Task item created successfully", typeof(TaskItemResponse))]
        [SwaggerResponse(400, "Invalid task item data")]
        [SwaggerResponse(401, "Unauthorized access")]
        [SwaggerResponse(500, "Internal server error")]
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
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Update an existing task item",
            Description = "Update the details of an existing task item. Authorization required.")]
        [SwaggerResponse(200, "Task item updated successfully", typeof(TaskItemResponse))]
        [SwaggerResponse(400, "Invalid task item data", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized access")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> UpdateTaskItem([FromRoute] string id, [FromBody] TaskItemUpdateRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest(new Response { StatusCode = 400, Message = "ID in the URL does not match ID in the request body." });
            }
            var updatedTaskItem = await taskItemRepository.UpdateTaskItem(request);
            return StatusCode(updatedTaskItem.StatusCode, updatedTaskItem);
        }

        [HttpDelete("delete/id={id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Delete a task item",
            Description = "Delete a task item by its ID. Authorization required.")]
        [SwaggerResponse(200, "Task item deleted successfully", typeof(Response))]
        [SwaggerResponse(400, "Invalid task item ID", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized access")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> DeleteTaskItem([FromRoute] string id)
        {
            var deletedTaskItem = await taskItemRepository.DeleteTaskItem(id);
            return StatusCode(deletedTaskItem.StatusCode, deletedTaskItem);
        }
    }
}
