using EBookSpace.Models.DTOs.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EBookSpace.Repositories
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public UserOrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task ChangeOrderStatus(UpdateOrderStatusModel data)
        {
            var order = await _db.Orders.FindAsync(data.OrderId);
            if (order == null)
            {
                throw new InvalidOperationException($"order with id:{data.OrderId} does not found");
            }
            order.OrderStatusId = data.OrderStatusId;
            await _db.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _db.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatuses()
        {
            return await _db.OrderStatuses.ToListAsync();
        }

        public async Task TogglePaymentStatus(int orderId)
        {
            var order = await _db.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException($"order withi id:{orderId} does not found");
            }
            order.IsPaid = !order.IsPaid;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> UserOrders(string userId, bool getAll = false)
        {
            var orders = _db.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.OrderDetail)
                .ThenInclude(x => x.Book)
                .ThenInclude(x => x.Genre)
                .AsQueryable();

            if (!getAll)
            {
                orders = orders.Where(x => x.UserId == userId);
            }

            return await orders.ToListAsync();
        }

        //private string GetUserId()
        //{
        //    var principal = _httpContextAccessor.HttpContext?.User;

        //    if (principal == null)
        //        throw new Exception("HttpContext is null");

        //    var userId = _userManager.GetUserId(principal);

        //    if (string.IsNullOrEmpty(userId))
        //        throw new Exception("User is not logged-in");

        //    return userId;
        //}
        // it is displaying _usermanager is null

    }
}
