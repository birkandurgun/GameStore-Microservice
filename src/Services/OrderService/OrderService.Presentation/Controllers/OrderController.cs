using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Features.Order.Queries.GetOrderDetailById;

namespace OrderService.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiController
    {
        private readonly ISender _sender;

        public OrderController(ISender sender):base(sender)  => _sender = sender;

        [HttpGet("order-details/{orderId}")]
        public async Task<IActionResult> GetOrderDetailsById(Guid orderId)
        {
            var result = await _sender.Send(new GetOrderDetailByIdQuery { OrderId = orderId });
            return result.IsSuccess
            ? Ok(result)
                : HandleFailure(result);
        }
    }
}
