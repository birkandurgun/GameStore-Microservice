using System;
using GameService.Application.Interfaces.CommandQuery;

namespace GameService.Application.Features.Games.Commands.DeleteGame
{
    public class DeleteGameCommand : ICommand
    {
        public Guid GameId { get; set; }
    }
}
