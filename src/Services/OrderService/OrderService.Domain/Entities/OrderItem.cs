using Amazon.DynamoDBv2.DataModel;

namespace OrderService.Domain.Entities
{
    public class OrderItem
    {
        [DynamoDBProperty]
        public Guid GameId { get; set; }

        [DynamoDBProperty]
        public string GameName { get; set; }

        [DynamoDBProperty]
        public decimal UnitPrice { get; set; }
    }
}
