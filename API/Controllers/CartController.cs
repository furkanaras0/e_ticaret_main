using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entity;
using Microsoft.EntityFrameworkCore;
using API.DTO;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly DataContext _context;

    public CartController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<CartDTO>> GetCart()
    {
        return CartToDTO(await GetOrCreateCart());
    }

    [HttpPost]
    public async Task<ActionResult<Cart>> AddItemToCart(int productId, int quantity)
    {
        var cart = await GetOrCreateCart();

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null) return NotFound("The product is not in database");

        cart.AddItem(product, quantity);

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return CreatedAtAction(nameof(GetCart), CartToDTO(cart));

        return BadRequest(new ProblemDetails { Title = "The Product can not be added to cart" });
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteItemFromCart(int productId, int quantity)
    {
        var cart = await GetOrCreateCart();

        cart.DeleteItem(productId, quantity);

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return CreatedAtAction(nameof(GetCart), CartToDTO(cart));

        return BadRequest(new ProblemDetails { Title = "The Product can not be removed from cart" });
    }

    private async Task<Cart> GetOrCreateCart()
    {
        var cart = await _context.Carts
            .Include(i => i.CartItems)
            .ThenInclude(p => p.Product)
            .Where(i => i.CustomerId == Request.Cookies["customerId"])
            .FirstOrDefaultAsync();

        if (cart == null)
        {
            var customerId = Guid.NewGuid().ToString();

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(1),
                IsEssential = true
            };

            Response.Cookies.Append("customerId", customerId, cookieOptions);
            cart = new Cart { CustomerId = customerId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    private CartDTO CartToDTO(Cart cart)
    {
        return new CartDTO
        {
            CartId = cart.CartId,
            CustomerId = cart.CustomerId,
            CartItems = cart.CartItems.Select(i => new CartItemDTO
            {
                ProductId = i.ProductId,
                Name = i.Product.Name,
                Price = i.Product.Price,
                Quantity = i.Quantity,
                ImageUrl = i.Product.ImageUrl,
            }).ToList()
        };
    }
}
