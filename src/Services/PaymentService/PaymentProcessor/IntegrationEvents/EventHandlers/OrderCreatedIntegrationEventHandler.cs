using EventBus.Base.Events;
using EventBus.Base.Interfaces;
using PaymentProcessor.IntegrationEvents.Events;

namespace PaymentProcessor.IntegrationEvents.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IEventBus _eventBus;

        public OrderCreatedIntegrationEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(OrderCreatedIntegrationEvent integrationEvent)
        {
            // Payment Process

            bool isPaymentSuccess = true;
            //bool isPaymentSuccess = false;

            IntegrationEvent paymentEvent = isPaymentSuccess
                ? new OrderPaymentSuccessIntegrationEvent(integrationEvent.OrderId)
                : new OrderPaymentFailedIntegrationEvent(integrationEvent.OrderId,"An error occured!");

            Console.WriteLine("Event fired.");

            _eventBus.Publish(paymentEvent);

            Console.WriteLine("Published");

            return Task.CompletedTask;
        }
    }
}
