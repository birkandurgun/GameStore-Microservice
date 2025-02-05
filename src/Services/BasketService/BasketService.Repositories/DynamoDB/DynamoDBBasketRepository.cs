using Amazon.DynamoDBv2.DataModel;
using BasketService.Entities.Models;
using BasketService.Repositories.Interfaces;

namespace BasketService.Repositories.DynamoDB
{
    public class DynamoDBBasketRepository : IBasketRepository
    {
        private readonly IDynamoDBContext _context;

        public DynamoDBBasketRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<CustomerBasket> GetByIdAsync(Guid customerId)
        {
            return await _context.LoadAsync<CustomerBasket>(customerId);
        }

        public async Task<bool> CreateAsync(CustomerBasket basket)
        {
            try
            {
                await _context.SaveAsync(basket);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(CustomerBasket basket)
        {
            try
            {
                await _context.SaveAsync(basket);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
    }
}
