namespace OrderService.Application.Features.Order.Queries.GetOrderDetailById
{
    public class PaymentInformationDTO
    {
        public string CardNumber { get; set; }
        public string NameOnCard { get; set; }
        public int NumberOfInstallment { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
