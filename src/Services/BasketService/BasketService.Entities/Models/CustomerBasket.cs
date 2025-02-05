using Amazon.DynamoDBv2.DataModel;

namespace BasketService.Entities.Models
{
    [DynamoDBTable("CustomerBasket")]
    public class CustomerBasket
    {
        [DynamoDBHashKey(AttributeName = "customerId")]
        public Guid CustomerId { get; set; }
        
        [DynamoDBProperty]
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    }
}
