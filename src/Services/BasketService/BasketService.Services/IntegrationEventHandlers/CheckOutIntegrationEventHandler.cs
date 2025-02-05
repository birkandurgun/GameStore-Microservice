using BasketService.Entities.IntegrationEvents;
using BasketService.Services.Interfaces;
using EventBus.Base.Interfaces;

namespace BasketService.Services.IntegrationEventHandlers
{
    public class CheckOutIntegrationEventHandler : IIntegrationEventHandler<CheckOutIntegrationEvent>
    {
        private readonly IBasketService _basketService;

        public CheckOutIntegrationEventHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task Handle(CheckOutIntegrationEvent integrationEvent)
        {
            await _basketService.ClearBasketAsync(integrationEvent.CustomerId);
        }
    }
}
