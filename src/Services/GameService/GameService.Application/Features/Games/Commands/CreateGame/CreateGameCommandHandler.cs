using GameService.Application.Interfaces.CommandQuery;
using GameService.Application.Interfaces.UnitOfWorks;
using GameService.Application.Shared;
using GameService.Domain.Entities;

namespace GameService.Application.Features.Games.Commands.CreateGame
{
    public class CreateGameCommandHandler : ICommandHandler<CreateGameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateGameCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var newGame = new Game
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PublisherId = request.PublisherId,
                GameGenres = request.GenreIds.Select(genreId => new GameGenre
                {
                    GameId = Guid.NewGuid(),
                    GenreId = genreId
                }).ToList()
            };

            await _unitOfWork.WriteRepository<Game>().AddAsync(newGame);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
