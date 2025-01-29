using IdentityService.Api.DTOs;

namespace IdentityService.Api.Services.User
{
    public interface IAppUserService
    {
        Task<RegisterResponseDTO> RegisterAsync(RegisterDTO registerDto);
        Task<LoginResponseDTO> LoginAsync(LoginDTO loginDto);
    }
}
