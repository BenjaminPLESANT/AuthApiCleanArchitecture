using AuthApi.Domain.Entities;
using AuthApi.Domain.Interfaces;
using AuthApi.Infrastructure.Identity;
using AuthApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Infrastructure.Repositories;


public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task AddUserAsync(User user, string password)
    {
        var appUser = new ApplicationUser
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };

        var result = await _userManager.CreateAsync(appUser, password);
        if (!result.Succeeded)
        {
            throw new Exception($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        var appUser = await _userManager.FindByIdAsync(user.Id.ToString());
        if (appUser == null) return false;

        return await _userManager.CheckPasswordAsync(appUser, password);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var appUser = await _userManager.FindByEmailAsync(email);
        if (appUser == null) return null;

        return new User
        {
            Id = appUser.Id,
            UserName = appUser.UserName,
            Email = appUser.Email
        };
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        var appUser = await _userManager.FindByIdAsync(userId.ToString());
        if (appUser == null) return null;

        return new User
        {
            Id = appUser.Id,
            UserName = appUser.UserName,
            Email = appUser.Email
        };
    }
}
