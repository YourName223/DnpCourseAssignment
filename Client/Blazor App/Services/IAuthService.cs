using System.Security.Claims;
using DTOs;

public interface IAuthService
{
    
    public Task LoginAsync(UserLoginDto request);
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Task<int?> GetUserIdAsync();
    public string? Jwt { get; }
    public event Action<ClaimsPrincipal>? OnAuthStateChanged;
}