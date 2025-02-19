using MediatR;
using OrderService.Application.Shared;

namespace OrderService.Application.Features.Order.Queries.GetOrderDetailById
{
    public class GetOrderDetailByIdQuery : IRequest<Result<GetOrderDetailByIdQueryResponse>>
    {
        public Guid OrderId { get; set; }
    }
}
