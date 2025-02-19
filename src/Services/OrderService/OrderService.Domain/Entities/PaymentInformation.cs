using Amazon.DynamoDBv2.DataModel;

namespace OrderService.Domain.Entities
{
    public class PaymentInformation
    {
        [DynamoDBProperty]
        public string CardNumber { get; set; }
        [DynamoDBProperty]
        public string NameOnCard { get; set; }
        [DynamoDBProperty]
        public int NumberOfInstallment { get; set; }
        [DynamoDBProperty]
        public decimal TotalPrice { get; set; }

    }
}
