using API.Application.DTOs;
using API.Application.DTOs.Auth;
using API.Application.DTOs.User;

namespace API.Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<UserResponse> GetAllUser();
        Task<UserResponse?> GetUserById(string id);
        Task<UserResponse?> GetUserByEmail(string email);
        Task<UserResponse?> GetUserByUsername(string username);
        Task<Response> UpdateUser(UserUpdateRequest updateRequest);
        Task<Response> ChangePassword(ChangePassword change);
        Task<Response> DeleteUser(string id);
    }
}
