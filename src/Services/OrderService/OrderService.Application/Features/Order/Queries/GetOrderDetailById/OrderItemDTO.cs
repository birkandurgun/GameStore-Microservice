namespace OrderService.Application.Features.Order.Queries.GetOrderDetailById
{
    public class OrderItemDTO
    {
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public decimal Price { get; set; }
    }
}
