using GameService.Application.Interfaces.CommandQuery;

namespace GameService.Application.Features.Games.Commands.UpdateGame
{
    public class UpdateGameCommand : ICommand
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid PublisherId { get; set; }
        public List<Guid> GenreIds { get; set; } = new List<Guid>();
    }
}
