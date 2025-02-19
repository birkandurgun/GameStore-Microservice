using Amazon.DynamoDBv2.DataModel;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Repositories.DynamoDB
{
    public class DynamoDBOrderRepository : IOrderRepository
    {
        private readonly IDynamoDBContext _context;

        public DynamoDBOrderRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _context.SaveAsync(order);
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            await _context.DeleteAsync(orderId);
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.LoadAsync<Order>(orderId);
        }
    }
}
