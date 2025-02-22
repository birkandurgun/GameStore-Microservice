using Amazon.DynamoDBv2.DataModel;
using EventBus.Base.Interfaces;
using LibraryService.Api.IntegrationEvents.Events;
using LibraryService.Api.Models;

namespace LibraryService.Api.IntegrationEvents.EventHandlers
{
    public class OrderCompletedIntegrationEventHandler : IIntegrationEventHandler<OrderCompletedIntegrationEvent>
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public OrderCompletedIntegrationEventHandler(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task Handle(OrderCompletedIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.CustomerId;

            var userGames = await _dynamoDbContext.LoadAsync<UserGames>(userId);

            if (userGames == null)
            {
                userGames = new UserGames
                {
                    UserId = userId,
                    Games = new List<LibraryItem>()
                };
            }

            foreach (var game in integrationEvent.Games)
            {
                if (!userGames.Games.Any(g => g.GameId == game.GameId))
                {
                    userGames.Games.Add(new LibraryItem
                    {
                        GameId = game.GameId,
                        GameName = game.GameName
                    });
                }
            }

            await _dynamoDbContext.SaveAsync(userGames);
        }
    }
}
