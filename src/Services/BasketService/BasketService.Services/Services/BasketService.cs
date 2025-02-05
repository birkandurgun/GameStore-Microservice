using BasketService.Entities.DTOs;
using BasketService.Entities.IntegrationEvents;
using BasketService.Entities.Models;
using BasketService.Entities.Shared;
using BasketService.Repositories.Interfaces;
using BasketService.Services.Interfaces;
using EventBus.Base.Interfaces;

namespace BasketService.Services.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IEventBus _eventBus;

        public BasketService(IBasketRepository basketRepository, IEventBus eventBus)
        {
            _basketRepository = basketRepository;
            _eventBus = eventBus;
        }

        public async Task<Result<CustomerBasket>> GetBasketAsync(Guid customerId)
        {
            try
            {
                if (customerId == Guid.Empty)
                    return Result.Fail<CustomerBasket>(
                        new Error("Customer ID cannot be empty.")
                    );

                var basket = await _basketRepository.GetByIdAsync(customerId);


                if (basket == null)
                {
                    basket = new CustomerBasket
                    {
                        CustomerId = customerId,
                        Items = new List<BasketItem>()
                    };

                    await _basketRepository.CreateAsync(basket);
                }

                return Result.Ok(basket);

            }
            catch (Exception ex)
            {
                return Result.Fail<CustomerBasket>(
                    new Error("Failed to retrieve basket.")
                );
            }
        }

        public async Task<Result> AddItemToBasketAsync(Guid customerId, BasketItem item)
        {
            try
            {
                if (customerId == Guid.Empty)
                    return Result.Fail(new Error("Customer ID cannot be empty."));

                if (item == null)
                    return Result.Fail(new Error("Item cannot be null."));

                var basket = await _basketRepository.GetByIdAsync(customerId);

                if (basket == null)
                {
                    basket = new CustomerBasket
                    {
                        CustomerId = customerId,
                        Items = new List<BasketItem> { item }
                    };

                    var created = await _basketRepository.CreateAsync(basket);
                    if (!created)
                        return Result.Fail(new Error("Failed to create a new basket with the item."));
                }
                else
                {
                    var existingItem = basket.Items.FirstOrDefault(i => i.GameId == item.GameId);

                    if (existingItem != null)
                        return Result.Fail(new Error("Item already exists in the basket."));

                    basket.Items.Add(item);

                    var updated = await _basketRepository.UpdateAsync(basket);
                    if (!updated)
                        return Result.Fail(new Error("Failed to update the basket with the new item."));
                }

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("An error occurred while adding the item to the basket."));
            }
        }


        public async Task<Result> ClearBasketAsync(Guid customerId)
        {
            try
            {
                if (customerId == Guid.Empty)
                    return Result.Fail(new Error("Customer ID cannot be empty."));

                var basket = await _basketRepository.GetByIdAsync(customerId);

                if (basket == null)
                    return Result.Ok();

                basket.Items.Clear();

                var updated = await _basketRepository.UpdateAsync(basket);

                if (!updated)
                    return Result.Fail(new Error("Failed to clear the basket."));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("An error occurred while clearing the basket."));
            }
        }


        public async Task<Result> CreateBasketAsync(CustomerBasket basket)
        {
            try
            {
                if (basket == null || basket.CustomerId == Guid.Empty)
                    return Result.Fail(new Error("Invalid basket or Customer ID."));

                var existingBasket = await _basketRepository.GetByIdAsync(basket.CustomerId);
                if (existingBasket != null)
                    return Result.Fail(new Error("A basket already exists for this customer."));

                var created = await _basketRepository.CreateAsync(basket);
                if (!created)
                    return Result.Fail(new Error("Failed to create the basket."));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("An error occurred while creating the basket."));
            }
        }

        public async Task<Result> RemoveItemFromBasketAsync(Guid customerId, Guid gameId)
        {
            try
            {
                if (customerId == Guid.Empty)
                    return Result.Fail(new Error("Customer ID cannot be empty."));

                if (gameId == Guid.Empty)
                    return Result.Fail(new Error("Product ID cannot be empty."));

                var basket = await _basketRepository.GetByIdAsync(customerId);
                if (basket == null)
                    return Result.Fail(new Error("Basket not found."));

                var itemToRemove = basket.Items.FirstOrDefault(item => item.GameId == gameId);
                if (itemToRemove == null)
                    return Result.Fail(new Error("Item not found in the basket."));

                basket.Items.Remove(itemToRemove);

                var updated = await _basketRepository.UpdateAsync(basket);
                if (!updated)
                    return Result.Fail(new Error("Failed to remove the item from the basket."));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("An error occurred while removing the item from the basket."));
            }
        }

        public async Task<Result> CheckOutAsync(Guid customerId, string userName, CheckoutDTO checkoutDTO)
        {
            var basketResult = await GetBasketAsync(customerId);

            if (!basketResult.IsSuccess || basketResult.Value == null)
            {
                return Result.Fail(new Error("Basket not found."));
            }
            var basket = basketResult.Value;

            var @event = new CheckOutIntegrationEvent
            {
                CustomerId = customerId,
                UserName = userName,
                NameOnCard = checkoutDTO.NameOnCard,
                CardNumber = checkoutDTO.CardNumber,
                CardExpiration = checkoutDTO.CardExpiration,
                CVV = checkoutDTO.CVV,
                Basket = basket
            };

            _eventBus.Publish(@event);

            return Result.Ok();
        }
    }
}
