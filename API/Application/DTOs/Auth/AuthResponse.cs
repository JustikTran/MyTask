namespace API.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public bool IsSuccess { get; set; } = string.IsNullOrEmpty(nameof(Token));
        public string? Message { get; set; } = string.Empty;
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
        public string? Token { get; set; } = string.Empty;
    }
}
