using AuthApi.Application.DTOs;
using AuthApi.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using AuthApi.Domain.Interfaces;
using AuthApi.Domain.Entities;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> LoginAsync(LoginRequestDto request)
    {
         if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            throw new ArgumentException("Email and password are required");

        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        
        if (user == null || !await _userRepository.CheckPasswordAsync(user, request.Password))
            throw new InvalidCredentialsException();

        return _jwtTokenGenerator.GenerateJwtToken(user);
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {

         if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.UserName))
            throw new ArgumentException("Invalid input data");

        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
        
        if (existingUser != null)
            throw new Exception("User already exists");
            
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email
        };

        await _userRepository.AddUserAsync(user, request.Password);
    }
}
