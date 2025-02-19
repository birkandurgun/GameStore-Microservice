using EventBus.Base.Interfaces;
using MediatR;
using OrderService.Application.Features.Order.Commands.CreateOrder;
using OrderService.Domain.Entities;
using OrderService.Presentation.IntegrationEvents.Events;

namespace OrderService.Presentation.IntegrationEvents.EventHandlers
{
    public class CheckOutIntegrationEventHandler : IIntegrationEventHandler<CheckOutIntegrationEvent>
    {
        private readonly ISender _sender;

        public CheckOutIntegrationEventHandler(ISender sender)
        {
            _sender = sender;
        }

        public async Task Handle(CheckOutIntegrationEvent integrationEvent)
        {
            decimal totalPrice = integrationEvent.Basket?.Items?.Sum(item => item.UnitPrice) ?? 0;

            PaymentInformation paymentInformation = new PaymentInformation
            {
                CardNumber = integrationEvent.CardNumber,
                NameOnCard = integrationEvent.NameOnCard,
                NumberOfInstallment = 1,
                TotalPrice = totalPrice
            };

            var orderItems = integrationEvent.Basket.Items.Select(item => new CreateOrderItemDTO
            {
                GameId = item.GameId,
                GameName = item.GameName,
                UnitPrice = item.UnitPrice
            }).ToList();

            CreateOrderCommand command = new CreateOrderCommand
            {
                CustomerId = integrationEvent.CustomerId,
                OrderItems = orderItems,
                PaymentInformation = paymentInformation
            };

            await _sender.Send(command);
        }
    }
}
