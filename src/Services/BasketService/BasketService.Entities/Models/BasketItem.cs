using Amazon.DynamoDBv2.DataModel;

namespace BasketService.Entities.Models
{
    public class BasketItem
    {
        [DynamoDBProperty]
        public Guid Id { get; set; }

        [DynamoDBProperty]
        public Guid GameId { get; set; }

        [DynamoDBProperty]
        public string GameName { get; set; }

        [DynamoDBProperty]
        public decimal UnitPrice { get; set; }

    }
}
