using GameService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(g => g.Description)
                .HasMaxLength(1000);

            builder.Property(g => g.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(g => g.Publisher)
                .WithMany(p => p.Games)
                .HasForeignKey(g => g.PublisherId);

            builder.HasMany(g => g.GameGenres)
                .WithOne(gc => gc.Game)
                .HasForeignKey(gc => gc.GameId);
        }
    }
}
