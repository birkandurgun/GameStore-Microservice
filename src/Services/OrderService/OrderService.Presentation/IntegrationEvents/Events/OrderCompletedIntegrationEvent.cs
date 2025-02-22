using EventBus.Base.Events;

namespace OrderService.Presentation.IntegrationEvents.Events
{
    public class OrderCompletedIntegrationEvent : IntegrationEvent
    {
        public Guid CustomerId { get; set; }
        public List<OrderCompletedGameItem> Games { get; set; } = new List<OrderCompletedGameItem>();
    }

    public class OrderCompletedGameItem
    {
        public Guid GameId { get; set; }
        public string GameName { get; set; }

    }
}
