using GameService.Application.Interfaces.CommandQuery;
using GameService.Application.Shared;

namespace GameService.Application.Features.Games.Queries.GetGameDetails
{
    public class GetGameDetailsQuery : IQuery<GetGameDetailsQueryResponse>
    {
        public Guid GameId { get; set; }
    }
}
