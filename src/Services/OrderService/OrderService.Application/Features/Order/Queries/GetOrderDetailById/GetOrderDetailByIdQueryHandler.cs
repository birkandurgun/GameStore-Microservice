using MediatR;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Shared;

namespace OrderService.Application.Features.Order.Queries.GetOrderDetailById
{
    public class GetOrderDetailByIdQueryHandler : IRequestHandler<GetOrderDetailByIdQuery, Result<GetOrderDetailByIdQueryResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderDetailByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<GetOrderDetailByIdQueryResponse>> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);

            return Result.Ok(new GetOrderDetailByIdQueryResponse
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                Items = order.Items?.Select(item => new OrderItemDTO
                {
                    GameId = item.GameId,
                    GameName = item.GameName,
                    Price = item.UnitPrice
                }).ToList(),
                PaymentInformation = order.PaymentInformation == null ? null : new PaymentInformationDTO
                {
                    CardNumber = order.PaymentInformation.CardNumber,
                    NameOnCard = order.PaymentInformation.NameOnCard,
                    NumberOfInstallment = order.PaymentInformation.NumberOfInstallment,
                    TotalPrice = order.PaymentInformation.TotalPrice
                }
            });
        }
    }
}
