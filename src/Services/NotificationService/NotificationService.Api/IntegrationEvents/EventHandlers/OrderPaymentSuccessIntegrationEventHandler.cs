using EventBus.Base.Interfaces;
using NotificationService.Api.IntegrationEvents.Events;

namespace NotificationService.Api.IntegrationEvents.EventHandlers
{
    public class OrderPaymentSuccessIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentSuccessIntegrationEvent>
    {
        public Task Handle(OrderPaymentSuccessIntegrationEvent integrationEvent)
        {
            Console.WriteLine($"Order Payment Success \nOrderId: {integrationEvent.OrderId}");

            return Task.CompletedTask;
        }
    }
}
