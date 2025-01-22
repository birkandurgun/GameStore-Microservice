using GameService.Domain.Entities.Common;

namespace GameService.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }


        public Guid PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
    }
}
