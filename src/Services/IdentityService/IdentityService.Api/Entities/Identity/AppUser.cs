using Microsoft.AspNetCore.Identity;

namespace IdentityService.Api.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
