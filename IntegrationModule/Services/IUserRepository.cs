using IntegrationModule.Models;

namespace IntegrationModule.Services
{
    public interface IUserRepository
    {
        User Add(UserRegisterRequest request);
        Tokens JwtTokens(JwtTokensRequest request);
    }
}
