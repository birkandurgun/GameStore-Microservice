using GameService.Application.Interfaces.CommandQuery;
using GameService.Application.Interfaces.UnitOfWorks;
using GameService.Application.Shared;
using GameService.Domain.Entities;
using System.Linq.Expressions;

namespace GameService.Application.Features.Games.Queries.GetAllGamesWithPaging
{
    public class GetAllGamesWithPagingQueryHandler : IQueryHandler<GetAllGamesWithPagingQuery, PaginatedResult<GetAllGamesWithPagingResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllGamesWithPagingQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaginatedResult<GetAllGamesWithPagingResponse>>> Handle(GetAllGamesWithPagingQuery request, CancellationToken cancellationToken)
        {
            var predicates = new List<Expression<Func<Game, bool>>>();
            var includes = new Expression<Func<Game, object>>[] { g => g.Publisher, g => g.GameGenres };

            if (!string.IsNullOrEmpty(request.SearchWord))
                predicates.Add(g => g.Name.Contains(request.SearchWord) || g.Description.Contains(request.SearchWord));

            if (request.GenreId.HasValue)
                predicates.Add(g => g.GameGenres.Any(gg => gg.GenreId == request.GenreId.Value));

            if (request.PublisherId.HasValue)
                predicates.Add(g => g.PublisherId == request.PublisherId.Value);

            if (request.MinPrice.HasValue)
                predicates.Add(g => g.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                predicates.Add(g => g.Price <= request.MaxPrice.Value);

            Expression<Func<Game, object>>? orderBy = null;
            bool isDescending = false;

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                switch (request.SortBy.ToLower())
                {
                    case "name":
                        orderBy = g => g.Name;
                        break;
                    case "price":
                        orderBy = g => g.Price;
                        break;
                    case "publisher":
                        orderBy = g => g.Publisher.Name;
                        break;
                    default:
                        orderBy = g => g.Name;
                        break;
                }

                isDescending = request.SortOrder?.ToLower() == "desc";
            }

            var games = await _unitOfWork.ReadRepository<Game>()
                .GetWithPaginationAsync(
                    page: request.PageNumber,
                    pageSize: request.PageSize,
                    enableTracking: false,
                    orderBy: orderBy,
                    isDescending: isDescending,
                    predicates: predicates.ToArray(),
                    includes: includes
                );

            var gameResponses = games.Data.Select(g => new GetAllGamesWithPagingResponse
            {
                Name = g.Name,
                Description = g.Description.Length > 50
                    ? g.Description.Substring(0, 50) + "..."
                    : g.Description,
                Price = g.Price,
                PublisherName = g.Publisher.Name
            }).ToList();

            var result = new PaginatedResult<GetAllGamesWithPagingResponse>
            {
                TotalCount = games.TotalCount,
                TotalPages = (int)Math.Ceiling(games.TotalCount / (double)request.PageSize),
                Page = request.PageNumber,
                PageSize = request.PageSize,
                Data = gameResponses
            };

            return Result.Ok(result);
        }
    }
}
