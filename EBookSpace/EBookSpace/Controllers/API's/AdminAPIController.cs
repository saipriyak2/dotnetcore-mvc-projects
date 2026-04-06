using EBookSpace.Mappers;
using EBookSpace.Models;
using EBookSpace.Models.DTOs;
using EBookSpace.Models.DTOs.API.AdminOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EBookSpace.Controllers.API
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminOperationsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminOperationsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/admin/orders
        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetail)
                .ThenInclude(od => od.Book)
                .Where(o => !o.IsDeleted)
                .OrderByDescending(o => o.CreateDate)
                .ToListAsync();

            return Ok(orders.Select(o => o.ToAdminOrderDto()));
        }

        // 🔹 PUT: api/admin/orders/{id}
        [HttpPut("orders/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);

            if (order == null)
                return NotFound("Order not found");

            order.OrderStatusId = dto.OrderStatusId;
            order.IsPaid = dto.IsPaid;

            await _context.SaveChangesAsync();

            return Ok("Order updated successfully");
        }
    }
}


