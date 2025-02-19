using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task CreateOrderAsync(Order order);
        Task DeleteOrderAsync(Guid orderId);
    }
}
