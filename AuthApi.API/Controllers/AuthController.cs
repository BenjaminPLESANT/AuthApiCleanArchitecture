using AuthApi.Application.DTOs;
using AuthApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) =>_authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        await _authService.RegisterAsync(request);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var token = await _authService.LoginAsync(request);
        if (token == null)
            return Unauthorized();

        return Ok(new { Token = token });
    }

    /**
         * Auhtorize garantit que la route GetFakeData ne sera accessible qu'aux utilisateurs connectés
         */
    [Authorize]
    [HttpGet("fake-data")]
    public IActionResult GetFakeData()
    {
        var fakeData = new List<object>
            {
                new { Id = 1, Name = "Alice", Age = 30, Email = "alice@example.com" },
                new { Id = 2, Name = "Bob", Age = 25, Email = "bob@example.com" },
                new { Id = 3, Name = "Charlie", Age = 35, Email = "charlie@example.com" },
            };

        return Ok(fakeData);
    }
}
