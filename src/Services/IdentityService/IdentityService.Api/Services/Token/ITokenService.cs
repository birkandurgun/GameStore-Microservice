namespace IdentityService.Api.Services.Token
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(string userId, string email, IList<string> roles);
    }
}
