using GameService.Application.Interfaces.CommandQuery;
using GameService.Application.Interfaces.UnitOfWorks;
using GameService.Application.Shared;
using GameService.Domain.Entities;
using System.Linq.Expressions;

namespace GameService.Application.Features.Games.Queries.GetGameDetails
{
    public class GetGameDetailsQueryHandler : IQueryHandler<GetGameDetailsQuery, GetGameDetailsQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGameDetailsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetGameDetailsQueryResponse>> Handle(GetGameDetailsQuery request, CancellationToken cancellationToken)
        {
            var includes = new Expression<Func<Game, object>>[]
            {
                g => g.Publisher,
                g => g.GameGenres
            };

            var game = await _unitOfWork.ReadRepository<Game>()
                .GetByIdAsync(request.GameId.ToString(),includes: includes);

            var genreIds = game.GameGenres.Select(gc => gc.GenreId).ToList();

            var predicate = new List<Expression<Func<Genre, bool>>>();
            predicate.Add(g => genreIds.Contains(g.Id));

            var genres = _unitOfWork.ReadRepository<Genre>()
                .GetWhere(predicate.ToArray());

            var response = new GetGameDetailsQueryResponse
            {
                Name = game.Name,
                Description = game.Description,
                Price = game.Price,
                PublisherName = game.Publisher.Name,
                Genres = genres.Select(g => g.Name).ToList()
            };

            return Result.Ok(response);
        }
    }
}
