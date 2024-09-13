using AuthApi.Domain.Entities;

namespace AuthApi.Domain.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(User user);
    }
}
