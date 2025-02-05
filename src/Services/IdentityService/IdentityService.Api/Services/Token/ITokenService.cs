namespace IdentityService.Api.Services.Token
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(string userId, string email, string userName, IList<string> roles);
    }
}
