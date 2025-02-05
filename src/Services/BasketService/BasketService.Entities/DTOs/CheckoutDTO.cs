namespace BasketService.Entities.DTOs
{
    public class CheckoutDTO
    {
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public DateOnly CardExpiration { get; set; }
        public string CVV { get; set; }
        //public Guid CustomerId { get; set; }
    }
}
