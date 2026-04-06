using EBookSpace.Mappers;
using EBookSpace.Models;
using EBookSpace.Models.DTOs;
using EBookSpace.Models.DTOs.API.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace EBookSpace.Controllers.API
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class OrdersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/orders (user orders)
        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _context.Orders
                .Include(o => o.OrderDetail)
                .ThenInclude(od => od.Book)
                .Where(o => o.UserId == userId && !o.IsDeleted)
                .OrderByDescending(o => o.CreateDate)
                .ToListAsync();

            return Ok(orders.Select(o => o.ToOrderDto()));
        }

        // 🔹 GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = await _context.Orders
                .Include(o => o.OrderDetail)
                .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId && !o.IsDeleted);

            if (order == null)
                return NotFound("Order not found");

            return Ok(order.ToOrderDto());
        }

        // 🔹 POST: api/orders (Place Order)
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Include Book and ShoppingCart, then filter by ShoppingCart.UserId
            var cartItems = await _context.CartDetails
                .Include(c => c.Book)
                .Include(c => c.ShoppingCart)
                .Where(c => c.ShoppingCart.UserId == userId && !c.ShoppingCart.IsDeleted)
                .ToListAsync();

            if (!cartItems.Any())
                return BadRequest("Cart is empty");

            var order = new Order
            {
                UserId = userId,
                CreateDate = DateTime.UtcNow,
                OrderStatusId = 1, // assume 1 = Pending
                Name = dto.Name,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,
                Address = dto.Address,
                PaymentMethod = dto.PaymentMethod,
                IsPaid = false,
                OrderDetail = new List<OrderDetail>()
            };

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Book.Price
                };

                order.OrderDetail.Add(orderDetail);
            }

            await _context.Orders.AddAsync(order);

            // Clear cart after order
            _context.CartDetails.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            return Ok(order.ToOrderDto());
        }
    }
}
