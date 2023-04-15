using IntegrationModule.Models;

namespace IntegrationModule.Services
{
    public interface IUserRepository
    {
        User Add(UserRegisterRequest request);
        void ValidateEmail(ValidateEmailRequest request);
        Tokens JwtTokens(JwtTokensRequest request);
        void ChangePassword(ChangePasswordRequest request);
    }
}
