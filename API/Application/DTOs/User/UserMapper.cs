using API.Application.DTOs.Auth;

namespace API.Application.DTOs.User
{
    public class UserMapper
    {
        private static UserMapper? _instance;
        private static readonly object _lock = new object();
        public static UserMapper Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new UserMapper();
                    }
                    return _instance;
                }
            }
        }

        public UserResponse ToResponse(Domain.Entities.User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                Role = user.Role,
                CreateAt = user.CreateAt,
                UpdateAt = user.UpdateAt,
            };
        }

        public Domain.Entities.User ToEntity(RegisterRequest request)
        {
            return new Domain.Entities.User
            {
                Username = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsActive = false,
                Role = "User",
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                IsBan = false,
                IsDelete = false,
            };
        }
    }
}
