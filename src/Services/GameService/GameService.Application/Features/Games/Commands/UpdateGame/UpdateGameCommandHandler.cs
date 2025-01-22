using GameService.Application.Interfaces.CommandQuery;
using GameService.Application.Interfaces.UnitOfWorks;
using GameService.Application.Shared;
using GameService.Domain.Entities;

namespace GameService.Application.Features.Games.Commands.UpdateGame
{
    public class UpdateGameCommandHandler : ICommandHandler<UpdateGameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGameCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.ReadRepository<Game>()
                .GetByIdAsync(
                    request.GameId.ToString(),
                    enableTracking: true,
                    includes: g => g.GameGenres
                );

            if (game == null)
                return Result.Fail("Game not found.");

            game.Name = request.Name;
            game.Description = request.Description;
            game.Price = request.Price;
            game.PublisherId = request.PublisherId;

            game.GameGenres.Clear();
            game.GameGenres = request.GenreIds.Select(genreId => new GameGenre
            {
                GameId = game.Id,
                GenreId = genreId
            }).ToList();

            _unitOfWork.WriteRepository<Game>().Update(game);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
