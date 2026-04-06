using EBookSpace.Models;
using EBookSpace.Models.DTOs;
using EBookSpace.Models.DTOs.API.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using System;
using System.Security.Claims;
using System.Security.Claims;

namespace EBookSpace.Controllers.API_s
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize] // JWT Required
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 GET /api/cart
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _context.ShoppingCarts
                .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);

            if (cart == null)
                return Ok(new List<CartItemDTO>());

            var cartItems = cart.CartDetails.Select(c => new CartItemDTO
            {
                BookId = c.BookId,
                BookTitle = c.Book.BookName,
                Quantity = c.Quantity,
                Price = c.UnitPrice
            }).ToList();

            return Ok(cartItems);
        }

        // 🔹 POST /api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddCartItemDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Step 1: Get or create cart
            var cart = await _context.ShoppingCarts
                .Include(c => c.CartDetails)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    CartDetails = new List<CartDetail>()
                };

                await _context.ShoppingCarts.AddAsync(cart);
            }

            // Step 2: Check if item exists in cart
            var existingItem = cart.CartDetails
                .FirstOrDefault(cd => cd.BookId == dto.BookId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                var book = await _context.Books.FindAsync(dto.BookId);

                if (book == null)
                    return NotFound("Book not found");

                var cartDetail = new CartDetail
                {
                    BookId = dto.BookId,
                    Quantity = dto.Quantity,
                    UnitPrice = book.Price,
                    ShoppingCart = cart
                };

                cart.CartDetails.Add(cartDetail);
            }

            await _context.SaveChangesAsync();

            return Ok("Item added to cart");
        }

        // 🔹 POST /api/cart/remove
        [HttpPost("remove")]
        
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveCartItemDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Step 1: Get user's cart with details
            var cart = await _context.ShoppingCarts
                .Include(c => c.CartDetails)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);

            if (cart == null)
                return NotFound("Cart not found");

            // Step 2: Find the item in the cart
            var item = cart.CartDetails.FirstOrDefault(cd => cd.BookId == dto.BookId);

            if (item == null)
                return NotFound("Item not found in cart");

            // Step 3: Remove the item
            _context.CartDetails.Remove(item);
            await _context.SaveChangesAsync();

            return Ok("Item removed from cart");
        }
    }
}
