using GameService.Domain.Entities.Common;

namespace GameService.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
    }
}
