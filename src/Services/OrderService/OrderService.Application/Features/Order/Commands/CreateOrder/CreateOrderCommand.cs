using MediatR;
using OrderService.Application.Shared;
using OrderService.Domain.Entities;

namespace OrderService.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Result>
    {
        public Guid CustomerId { get; set; }
        public List<CreateOrderItemDTO> OrderItems { get; set; }
        public PaymentInformation PaymentInformation { get; set; }

    }
}
