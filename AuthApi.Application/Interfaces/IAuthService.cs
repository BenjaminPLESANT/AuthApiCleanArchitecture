using AuthApi.Application.DTOs;

namespace AuthApi.Application.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(LoginRequestDto request);
    Task RegisterAsync(RegisterRequestDto request);
}