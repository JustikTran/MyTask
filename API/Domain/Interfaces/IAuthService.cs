using API.Application.DTOs;
using API.Application.DTOs.Auth;

namespace API.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginRequest request);
        Task<Response> Register(RegisterRequest request);
        Task<Response> ForgotPassword(ForgotPassword request);
    }
}
