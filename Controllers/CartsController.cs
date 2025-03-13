using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesTest.Entities;

[ApiController]
[Route("api/carts")]
[Authorize]
public class CartsController : ControllerBase
{
    private readonly CartService _cartService;

    public CartsController(CartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetCart(Guid userId)
    {
        var cart = await _cartService.GetCartByUserIdAsync(userId);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPost("{userId}/items")]
    [Authorize]
    public async Task<IActionResult> AddItemToCart(Guid userId, [FromBody] CartItem item)
    {
        var cart = await _cartService.AddItemToCartAsync(userId, item);
        return Ok(cart);
    }

    [HttpDelete("{userId}/items/{productId}")]
    [Authorize]
    public async Task<IActionResult> RemoveItemFromCart(Guid userId, int productId)
    {
        var cart = await _cartService.RemoveItemFromCartAsync(userId, productId);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpDelete("{userId}")]
    [Authorize]
    public async Task<IActionResult> ClearCart(Guid userId)
    {
        var cart = await _cartService.ClearCartAsync(userId);
        if (cart == null) return NotFound();
        return Ok(cart);
    }
}
