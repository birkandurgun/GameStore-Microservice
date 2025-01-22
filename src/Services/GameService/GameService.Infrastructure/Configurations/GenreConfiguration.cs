using GameService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(c => c.GameGenres)
                .WithOne(gc => gc.Genre)
                .HasForeignKey(gc => gc.GenreId);

            builder.HasData(
                new Genre { Id = Guid.Parse("d17c7345-36d2-48c7-8000-1be1231e84e5"), Name = "Free To Play", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("a83ff907-1c4b-45ed-90bb-b1332c620e5f"), Name = "Early Access", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("7177b6e3-582d-47ad-8cc0-1809b9410db0"), Name = "Action", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("ee983b67-56ad-4f92-8125-9c1c740e76b9"), Name = "Adventure", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("b9fda6d2-8d9d-4a5e-975d-78b8ef3c5667"), Name = "Casual", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("a3a1e6cf-325f-4dbd-b3b0-b45f4a77e416"), Name = "Indie", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("c5c8f2a3-2c68-47f2-9507-9f90639a1d8b"), Name = "Massively Multiplayer", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("a51661ff-6012-42db-9cf9-5c68cfa7f038"), Name = "Racing", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("ed8bc02f-9cde-46b7-b035-cbd58f788fc4"), Name = "RPG", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("0535780e-307b-4046-9b59-e937b6a8c5c1"), Name = "Simulation", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("f51a7796-7a07-47cc-8c99-e105f1d7c0e0"), Name = "Sports", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Genre { Id = Guid.Parse("cf3d6d98-3b93-4d7f-aed6-e24e1d3ecf33"), Name = "Strategy", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) }
            );
        }
    }
}
