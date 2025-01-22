using GameService.Domain.Entities.Common;

namespace GameService.Domain.Entities
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
