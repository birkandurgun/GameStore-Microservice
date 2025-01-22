using GameService.Application.Interfaces.CommandQuery;
using GameService.Application.Interfaces.UnitOfWorks;
using GameService.Application.Shared;
using GameService.Domain.Entities;

namespace GameService.Application.Features.Games.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : ICommandHandler<DeleteGameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGameCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.ReadRepository<Game>().GetByIdAsync(request.GameId.ToString());

            if (game == null)
                return Result.Fail("Game not found.");

            _unitOfWork.WriteRepository<Game>().Delete(game);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
