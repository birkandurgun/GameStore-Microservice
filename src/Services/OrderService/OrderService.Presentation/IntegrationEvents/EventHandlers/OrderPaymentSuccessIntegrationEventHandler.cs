using EventBus.Base.Interfaces;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Presentation.IntegrationEvents.Events;

namespace OrderService.Presentation.IntegrationEvents.EventHandlers
{
    public class OrderPaymentSuccessIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentSuccessIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly IOrderRepository _orderRepository;

        public OrderPaymentSuccessIntegrationEventHandler(IEventBus eventBus, IOrderRepository orderRepository)
        {
            _eventBus = eventBus;
            _orderRepository = orderRepository;
        }

        public async Task Handle(OrderPaymentSuccessIntegrationEvent integrationEvent)
        {
            var order = await _orderRepository.GetOrderByIdAsync(integrationEvent.OrderId);

            var orderCompletedEvent = new OrderCompletedIntegrationEvent
            {
                CustomerId = order.CustomerId,
                Games = order.Items.Select(item => new OrderCompletedGameItem
                {
                    GameId = item.GameId,
                    GameName = item.GameName
                }).ToList()
            };

            _eventBus.Publish(orderCompletedEvent);
        }
    }
}
