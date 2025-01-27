using EventBus.Base.Interfaces;
using NotificationService.Api.IntegrationEvents.Events;

namespace NotificationService.Api.IntegrationEvents.EventHandlers
{
    public class OrderPaymentFailedIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {

        public Task Handle(OrderPaymentFailedIntegrationEvent integrationEvent)
        {
            Console.WriteLine($"Order Payment Failed \nOrderId: {integrationEvent.OrderId} \nErrorMessage: {integrationEvent.ErrorMessage}");

            return Task.CompletedTask;
        }
    }
}
