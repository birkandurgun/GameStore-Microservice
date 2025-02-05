using BasketService.Entities.Models;
using EventBus.Base.Events;

namespace BasketService.Entities.IntegrationEvents
{
    public class CheckOutIntegrationEvent : IntegrationEvent
    {
        public Guid CustomerId { get; set; }
        public string UserName { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public DateOnly CardExpiration { get; set; }
        public string CVV { get; set; }
        public CustomerBasket Basket { get; set; }
    }
}
