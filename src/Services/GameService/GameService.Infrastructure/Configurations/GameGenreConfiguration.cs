using GameService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            builder.HasKey(gc => new { gc.GameId, gc.GenreId });

            builder.HasOne(gc => gc.Game)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gc => gc.GameId);

            builder.HasOne(gc => gc.Genre)
                .WithMany(c => c.GameGenres)
                .HasForeignKey(gc => gc.GenreId);
        }
    }
}
