using API.Application.DTOs;
using API.Application.DTOs.Auth;
using API.Application.DTOs.User;
using API.Domain.Interfaces;
using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Response> ChangePassword(ChangePassword change)
        {
            try
            {
                var existing = await context.Users.FindAsync(Guid.Parse(change.Id));
                if (existing == null || existing.IsDelete)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                if (!BCrypt.Net.BCrypt.Verify(change.CurrentPassword, existing.Password))
                {
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Old password is incorrect"
                    };
                }
                existing.Password = BCrypt.Net.BCrypt.HashPassword(change.NewPassword);
                existing.UpdateAt = DateTime.UtcNow;
                context.Users.Update(existing);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Change password successfully"
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

        public async Task<Response> DeleteUser(string id)
        {
            try
            {
                var existing = await context.Users.FindAsync(Guid.Parse(id));
                if (existing == null || existing.IsDelete)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                existing.IsDelete = true;
                existing.UpdateAt = DateTime.UtcNow;
                context.Users.Update(existing);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Delete user successfully"
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

        public IQueryable<UserResponse> GetAllUser()
        {
            try
            {
                var listUser = context.Users.ToList();
                return listUser
                    .Select(UserMapper.Instance.ToResponse)
                    .AsQueryable();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message.ToString());
            }
        }

        public async Task<UserResponse?> GetUserByEmail(string email)
        {
            try
            {
                var existing = await context.Users
                    .Where(u => u.Email == email)
                    .FirstOrDefaultAsync();
                if (existing == null) return null;
                return UserMapper.Instance.ToResponse(existing);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message.ToString());
            }
        }

        public async Task<UserResponse?> GetUserById(string id)
        {
            try
            {
                var existing = await context.Users.FindAsync(Guid.Parse(id));
                if (existing == null) return null;
                return UserMapper.Instance.ToResponse(existing);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message.ToString());
            }
        }

        public async Task<UserResponse?> GetUserByUsername(string username)
        {
            try
            {
                var existing = await context.Users
                    .Where(u => u.Username == username)
                    .FirstOrDefaultAsync();
                if (existing == null) return null;
                return UserMapper.Instance.ToResponse(existing);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message.ToString());
            }
        }

        public async Task<Response> UpdateUser(UserUpdateRequest updateRequest)
        {
            try
            {
                var existing = await context.Users.FindAsync(Guid.Parse(updateRequest.Id));
                if (existing == null || existing.IsDelete)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                existing.Username = updateRequest.Username;
                existing.Email = updateRequest.Email;
                existing.FirstName = updateRequest.FirstName;
                existing.LastName = updateRequest.LastName;
                existing.UpdateAt = DateTime.UtcNow;
                context.Users.Update(existing);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Update user successfully"
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
