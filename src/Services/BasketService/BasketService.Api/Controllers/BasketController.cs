using BasketService.Entities.DTOs;
using BasketService.Entities.Models;
using BasketService.Services.Interfaces;
using EventBus.Base.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IUserContextService _userContextService;

        public BasketController(IBasketService basketService, IUserContextService userContextService, IEventBus eventBus)
        {
            _basketService = basketService;
            _userContextService = userContextService;
        }

        [HttpGet("get-basket")]
        public async Task<IActionResult> GetBasket()
        {
            var customerId = _userContextService.GetUserId();
            var result = await _basketService.GetBasketAsync(customerId);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem([FromBody] BasketItem item)
        {
            var customerId = _userContextService.GetUserId();
            var result = await _basketService.AddItemToBasketAsync(customerId, item);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpDelete("remove-item/{itemId}")]
        public async Task<IActionResult> RemoveItem(Guid itemId)
        {
            var customerId = _userContextService.GetUserId();
            var result = await _basketService.RemoveItemFromBasketAsync(customerId, itemId);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpPost("clear-basket")]
        public async Task<IActionResult> ClearBasket()
        {
            var customerId = _userContextService.GetUserId();
            var result = await _basketService.ClearBasketAsync(customerId);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckoutDTO checkoutDTO)
        {
            var customerId = _userContextService.GetUserId();
            var userName = _userContextService.GetUsername();

            var result = await _basketService.CheckOutAsync(customerId, userName, checkoutDTO);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
