using EventBus.Base.Events;

namespace PaymentProcessor.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public OrderCreatedIntegrationEvent()
        {
            
        }

        public OrderCreatedIntegrationEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
