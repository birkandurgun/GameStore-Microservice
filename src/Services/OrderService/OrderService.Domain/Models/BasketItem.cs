namespace OrderService.Domain.Models
{
    public class BasketItem
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }

        public string GameName { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
