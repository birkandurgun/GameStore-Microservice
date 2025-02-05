using BasketService.Entities.DTOs;
using BasketService.Entities.Models;
using BasketService.Entities.Shared;

namespace BasketService.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Result<CustomerBasket>> GetBasketAsync(Guid customerId);
        Task<Result> CreateBasketAsync(CustomerBasket basket);
        Task<Result> AddItemToBasketAsync(Guid customerId, BasketItem item);
        Task<Result> RemoveItemFromBasketAsync(Guid customerId, Guid gameId);
        Task<Result> ClearBasketAsync(Guid customerId);
        Task<Result> CheckOutAsync(Guid customerId, string userName, CheckoutDTO checkoutDTO);
    }
}
