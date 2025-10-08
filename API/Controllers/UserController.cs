using API.Application.DTOs;
using API.Application.DTOs.Auth;
using API.Application.DTOs.User;
using API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("odata/user")]
    [ApiController]
    public class UserController : ODataController
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Get all users",
            Description = "Retrieve a list of all users in the system. Admin access required.")]
        [SwaggerResponse(200, "List of users retrieved successfully", typeof(IEnumerable<UserResponse>))]
        public ActionResult<IEnumerable<UserResponse>> GetAllUser()
        {
            var users = userRepository.GetAllUser();
            return Ok(users);
        }

        [HttpGet("find/id={id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Get user by ID",
            Description = "Retrieve a user by their unique ID. Authorization required.")]
        [SwaggerResponse(200, "User found", typeof(UserResponse))]
        [SwaggerResponse(404, "User not found")]
        [SwaggerResponse(401, "Unauthorized access")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "User found.",
                Data = user
            });
        }

        [HttpGet("find/username={username}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Get user by username",
            Description = "Retrieve a user by their unique username. No authorization required.")]
        [SwaggerResponse(200, "User found", typeof(UserResponse))]
        [SwaggerResponse(404, "User not found")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> GetUserByUser([FromRoute] string username)
        {
            var user = await userRepository.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "User found.",
                Data = user
            });
        }


        [HttpGet("find/email={email}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Get user by email",
            Description = "Retrieve a user by their unique email address. No authorization required.")]
        [SwaggerResponse(200, "User found", typeof(UserResponse))]
        [SwaggerResponse(404, "User not found")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "User found.",
                Data = user
            });
        }

        [HttpPut("update/id={id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Update user information",
            Description = "Update user details such as name, email, etc. Authorization required.")]
        [SwaggerResponse(200, "User updated successfully", typeof(Response))]
        [SwaggerResponse(400, "Invalid data or ID mismatch", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized access")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UserUpdateRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest(new Response { StatusCode = 400, Message = "Id in route and body do not match." });
            }
            var response = await userRepository.UpdateUser(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("change-pass/id={id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Change user password",
            Description = "Change the password for a user. Authorization required.")]
        [SwaggerResponse(200, "Password changed successfully", typeof(Response))]
        [SwaggerResponse(400, "Invalid data or ID mismatch", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized access")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> ChangePassword([FromRoute] string id, [FromBody] ChangePassword request)
        {
            if (id != request.Id)
            {
                return BadRequest(new Response { StatusCode = 400, Message = "Id in route and body do not match." });
            }
            var response = await userRepository.ChangePassword(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete/id={id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Delete a user",
            Description = "Delete a user by their unique ID. Authorization required.")]
        [SwaggerResponse(200, "User deleted successfully", typeof(Response))]
        [SwaggerResponse(400, "Invalid ID", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized access")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> DeleteItem([FromRoute] string id)
        {
            var response = await userRepository.DeleteUser(id);
            return StatusCode(response.StatusCode, response);
        }

    }
}
