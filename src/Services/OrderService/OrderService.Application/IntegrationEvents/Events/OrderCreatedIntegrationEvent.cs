using EventBus.Base.Events;

namespace OrderService.Application.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public OrderCreatedIntegrationEvent()
        {

        }

        public OrderCreatedIntegrationEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
