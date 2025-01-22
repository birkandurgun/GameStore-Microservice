using GameService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(p => p.Games)
                .WithOne(g => g.Publisher)
                .HasForeignKey(g => g.PublisherId);

            builder.HasData(
                new Publisher { Id = Guid.Parse("c68fe36e-746b-4d82-a9fc-e9fd16d2f6f2"), Name = "Valve", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Publisher { Id = Guid.Parse("cf8f595f-bb19-4c0d-b5b4-bfc859f580fb"), Name = "Electronic Arts", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Publisher { Id = Guid.Parse("22ed2c3e-7b7b-4f67-8a4f-4c79b7cf3209"), Name = "Ubisoft", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Publisher { Id = Guid.Parse("a0914389-bd9a-4737-a45d-b54b378b6d56"), Name = "Bethesda Softworks", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Publisher { Id = Guid.Parse("3f6e8791-10a0-4317-9879-79a1d3b18f0e"), Name = "CD Projekt", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Publisher { Id = Guid.Parse("dbf5bfa1-3d7d-47e4-883f-42b1c3184e92"), Name = "Activision", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Publisher { Id = Guid.Parse("c7a86de5-b230-41e2-b0fc-bb75be1301bc"), Name = "Take-Two Interactive", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Publisher { Id = Guid.Parse("595167fa-d015-44f3-8a27-0db55cb2efaf"), Name = "Square Enix", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Publisher { Id = Guid.Parse("e0c0f222-d9de-4ae7-bf0b-5d280473b8b4"), Name = "Bandai Namco Entertainment", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) },
                new Publisher { Id = Guid.Parse("7623a287-6012-429b-b53a-243da6e5f3b2"), Name = "Paradox Interactive", CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0) }
            );
        }
    }
}
