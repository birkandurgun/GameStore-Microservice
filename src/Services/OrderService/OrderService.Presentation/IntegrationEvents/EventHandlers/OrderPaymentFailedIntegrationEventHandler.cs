using EventBus.Base.Interfaces;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Presentation.IntegrationEvents.Events;

namespace OrderService.Presentation.IntegrationEvents.EventHandlers
{
    public class OrderPaymentFailedIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {
        private readonly IOrderRepository _repository;

        public OrderPaymentFailedIntegrationEventHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(OrderPaymentFailedIntegrationEvent integrationEvent)
        {
           await _repository.DeleteOrderAsync(integrationEvent.OrderId);
           Console.WriteLine($"Order with id {integrationEvent.OrderId} has deleted.");
        }
    }
}
