using EventBus.Base.Interfaces;
using MediatR;
using OrderService.Application.IntegrationEvents.Events;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Shared;
using OrderService.Domain.Entities;

namespace OrderService.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _eventBus;

        public CreateOrderCommandHandler(IEventBus eventBus, IOrderRepository orderRepository)
        {
            _eventBus = eventBus;
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Order order = new Domain.Entities.Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                OrderDate = DateTime.Now,
                PaymentInformation = request.PaymentInformation,
            };

            foreach(var orderItem in request.OrderItems)
            {
                var addOrderItem = new OrderItem
                {
                    GameId = orderItem.GameId,
                    GameName = orderItem.GameName,
                    UnitPrice = orderItem.UnitPrice
                };

                order.Items.Add(addOrderItem);
            }

            await _orderRepository.CreateOrderAsync(order);

            var orderCreatedIntegrationEvent = new OrderCreatedIntegrationEvent(order.OrderId);

            _eventBus.Publish(orderCreatedIntegrationEvent);

            return Result.Ok();
        }
    }
}
