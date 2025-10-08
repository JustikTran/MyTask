using API.Application.DTOs;
using API.Application.DTOs.Auth;
using API.Application.DTOs.User;
using API.Domain.Interfaces;
using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;
        private readonly TokenService tokenService;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.tokenService = new TokenService(configuration);
        }
        public async Task<Response> ForgotPassword(ForgotPassword request)
        {
            try
            {
                var exist = await context.Users.FindAsync(request.Username);
                if (exist == null || exist.IsDelete)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Account does not exist."
                    };
                }
                if (exist.IsBan)
                {
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Account is banned."
                    };
                }
                exist.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                exist.UpdateAt = DateTime.UtcNow;
                context.Users.Update(exist);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Password changed successfully."
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            try
            {
                var exist = await context.Users.FindAsync(request.Username);
                if (exist == null || exist.IsDelete)
                {
                    return new AuthResponse
                    {
                        Message = "Account does not exist."
                    };
                }
                if (exist.IsBan)
                {
                    return new AuthResponse
                    {
                        Message = "Account is banned."
                    };
                }
                if (!BCrypt.Net.BCrypt.Verify(request.Password, exist.Password))
                {
                    return new AuthResponse
                    {
                        Message = "Username or Password is incorrect."
                    };
                }
                var token = tokenService.GetToken(exist);
                return new AuthResponse
                {
                    Message = "Login successful.",
                    Token = token
                };
            }
            catch (Exception err)
            {
                return new AuthResponse
                {
                    Message = err.Message
                };
            }
        }

        public async Task<Response> Register(RegisterRequest request)
        {
            try
            {
                var account = await context.Users
                    .FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email);
                if (account != null)
                {
                    return new Response
                    {
                        StatusCode = 409,
                        Message = "Username or Email already exists."
                    };
                }
                var newAccount = UserMapper.Instance.ToEntity(request);
                context.Users.Add(newAccount);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Register successful."
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }
    }
}
