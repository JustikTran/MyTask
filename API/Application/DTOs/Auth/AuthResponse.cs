namespace API.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public bool IsSuccess { get; set; } = string.IsNullOrEmpty(nameof(Token));
        public string? Message { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
    }
}
