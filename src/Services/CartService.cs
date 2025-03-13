using Microsoft.EntityFrameworkCore;
using SalesTest.src.Entities;

public class CartService
{
    private readonly SaleDbContext _context;

    public CartService(SaleDbContext context)
    {
        _context = context;
    }

    public async Task<Cart>  GetCartByUserIdAsync(Guid userId)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<Cart> AddItemToCartAsync(Guid userId, CartItem item)
    {
        var cart = await GetCartByUserIdAsync(userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
        }

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            cart.Items.Add(item);
        }

        cart.CalculateTotal();
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart> RemoveItemFromCartAsync(Guid userId, int productId)
    {
        var cart = await GetCartByUserIdAsync(userId);
        if (cart == null) return null;

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            cart.Items.Remove(item);
            cart.CalculateTotal();
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<Cart> ClearCartAsync(Guid userId)
    {
        var cart = await GetCartByUserIdAsync(userId);
        if (cart == null) return null;

        cart.Items.Clear();
        cart.TotalAmount = 0;
        await _context.SaveChangesAsync();
        return cart;
    }
}
