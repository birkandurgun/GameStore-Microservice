namespace OrderService.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderItemDTO
    {
        public Guid GameId { get; set; }

        public string GameName { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
