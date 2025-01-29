using IdentityService.Api.Entities.Identity;
using IdentityService.Api.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Api.Contexts
{
    public class IdentityServiceDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public IdentityServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppRole>().HasData(
                new AppRole { Id = Guid.Parse("d1b85728-0bac-4c4e-b9d8-90180a8e50e6"), Name = Role.User.ToString(), NormalizedName = Role.User.ToString().ToUpper() },
                new AppRole { Id = Guid.Parse("2b6321ff-2be3-432f-8afd-fb49e430d9ed"), Name = Role.Admin.ToString(), NormalizedName = Role.Admin.ToString().ToUpper() }
            );
        }
    }
}
