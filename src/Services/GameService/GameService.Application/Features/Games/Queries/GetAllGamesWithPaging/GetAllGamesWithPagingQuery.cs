using GameService.Application.Interfaces.CommandQuery;
using GameService.Application.Shared;

namespace GameService.Application.Features.Games.Queries.GetAllGamesWithPaging
{
    public class GetAllGamesWithPagingQuery : IQuery<PaginatedResult<GetAllGamesWithPagingResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? SearchWord { get; set; }
        public Guid? GenreId { get; set; }
        public Guid? PublisherId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}
