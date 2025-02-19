using Amazon.DynamoDBv2.DataModel;

namespace OrderService.Domain.Entities
{
    [DynamoDBTable("Orders")]
    public class Order
    {
        [DynamoDBHashKey(AttributeName = "orderId")]
        public Guid OrderId { get; set; }

        [DynamoDBProperty]
        public DateTime OrderDate { get; set; }

        [DynamoDBProperty]
        public Guid CustomerId { get; set; }

        [DynamoDBProperty]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        [DynamoDBProperty]
        public PaymentInformation PaymentInformation { get; set; }
    }
}
