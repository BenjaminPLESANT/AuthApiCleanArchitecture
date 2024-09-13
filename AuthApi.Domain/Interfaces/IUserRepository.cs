using AuthApi.Domain.Entities;

namespace AuthApi.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(Guid userId);
    Task AddUserAsync(User user, string password);
    Task<User> GetUserByEmailAsync(string username);
    Task<bool> CheckPasswordAsync(User user, string password);
}