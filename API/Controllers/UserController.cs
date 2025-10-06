using API.Application.DTOs.Auth;
using API.Application.DTOs.User;
using API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

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
        public async Task<IActionResult> UpdateUser([FromRoute]string id, [FromBody] UserUpdateRequest request)
        {
            if(id != request.Id)
            {
                return BadRequest(new { Message = "Id in route and body do not match." });
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
        public async Task<IActionResult> ChangePassword([FromRoute] string id, [FromBody] ChangePassword request)
        {
            if (id != request.Id)
            {
                return BadRequest(new { Message = "Id in route and body do not match." });
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
        public async Task<IActionResult> DeleteItem([FromRoute]string id)
        {
            var response = await userRepository.DeleteUser(id);
            return StatusCode(response.StatusCode, response);
        }

    }
}
