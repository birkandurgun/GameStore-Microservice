using BasketService.Entities.Models;

namespace BasketService.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetByIdAsync(Guid customerId);
        Task<bool> CreateAsync(CustomerBasket basket);
        Task<bool> UpdateAsync(CustomerBasket basket);
    }
}
