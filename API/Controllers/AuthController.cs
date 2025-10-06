using API.Application.DTOs.Auth;
using API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(500)]
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
        [ProducesResponseType(500)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword request)
        {
            var response = await authService.ForgotPassword(request);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
