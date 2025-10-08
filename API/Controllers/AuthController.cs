using API.Application.DTOs;
using API.Application.DTOs.Auth;
using API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Login to the system",
            Description = "Return token JWT when login success.")]
        [SwaggerResponse(200, "Login success", typeof(AuthResponse))]
        [SwaggerResponse(401, "Incorrect information or account was banned.", typeof(Response))]

        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await authService.Login(request);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response);
        }


        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Register a new account",
            Description = "Create a new account with username, email and password.")]
        [SwaggerResponse(201, "Register success", typeof(Response))]
        [SwaggerResponse(400, "Invalid data register.", typeof(Response))]
        [SwaggerResponse(409, "Existing username or email.", typeof(Response))]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await authService.Register(request);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.StatusCode, response);
        }


        [HttpPut("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Forgot password",
            Description = "Change password with username and new password.")]
        [SwaggerResponse(200, "Change password success", typeof(Response))]
        [SwaggerResponse(400, "Account does not exist or account was banned.", typeof(Response))]
        [SwaggerResponse(404, "Account not found.", typeof(Response))]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword request)
        {
            var response = await authService.ForgotPassword(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
