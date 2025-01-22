using GameService.Application.Features.Games.Commands.CreateGame;
using GameService.Application.Features.Games.Commands.DeleteGame;
using GameService.Application.Features.Games.Commands.UpdateGame;
using GameService.Application.Features.Games.Queries.GetAllGamesWithPaging;
using GameService.Application.Features.Games.Queries.GetGameDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ApiController
    {
        private readonly ISender _sender;

        public GamesController(ISender sender) : base(sender) => _sender = sender;

        [HttpGet]
        public async Task<IActionResult> GetAllGamesWithPaging(
            [FromQuery] string? searchWord, [FromQuery] Guid? genreId, [FromQuery] Guid? publisherId,
            [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5,
            [FromQuery] string? sortBy = "name", [FromQuery] string? sortOrder = "asc")
        {
            var result = await _sender.Send(new GetAllGamesWithPagingQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize ,
                SearchWord = searchWord,
                GenreId = genreId,
                PublisherId = publisherId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SortBy = sortBy,
                SortOrder = sortOrder
            });

            return result.IsSuccess
                ? Ok(result)
                : HandleFailure(result);
        }

        [HttpGet]
        [Route("game-details/{gameId}")]
        public async Task<IActionResult> GetGameDetails([FromRoute] Guid gameId)
        {
            var result = await _sender.Send(new GetGameDetailsQuery{ GameId = gameId });
            return result.IsSuccess
                ? Ok(result)
                : HandleFailure(result);
        }

        [HttpPost]
        [Route("add-game")]
        public async Task<IActionResult> CreateGame(CreateGameCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess
                ? Ok(result)
                : HandleFailure(result);
        }

        [HttpPut]
        [Route("update-game")]
        public async Task<IActionResult> UpdateGame(UpdateGameCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess
                ? Ok(result)
                : HandleFailure(result);
        }

        [HttpDelete]
        [Route("delete-game")]
        public async Task<IActionResult> DeleteGame(DeleteGameCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess
                ? Ok(result)
                : HandleFailure(result);
        }
    }
}
