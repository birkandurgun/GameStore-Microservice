using BasketService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BasketService.Services.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(u => u.Type == ClaimTypes.Email).Value;
        }

        public List<string> GetRoles()
        {
            return _httpContextAccessor.HttpContext.User.FindAll(u => u.Type == ClaimTypes.Role)
                .Select(u => u.Value)
                .ToList();
        }

        public Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
        }

        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(u => u.Type == "UserName").Value;
        }
    }
}
