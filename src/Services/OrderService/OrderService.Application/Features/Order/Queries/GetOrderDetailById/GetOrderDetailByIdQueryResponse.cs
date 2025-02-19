namespace OrderService.Application.Features.Order.Queries.GetOrderDetailById
{
    public class GetOrderDetailByIdQueryResponse
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItemDTO> Items { get; set; }
        public PaymentInformationDTO PaymentInformation { get; set; }
    }
}
