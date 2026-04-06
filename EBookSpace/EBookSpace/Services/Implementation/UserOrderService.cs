using EBookSpace.Models.DTOs.UI;
using EBookSpace.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EBookSpace.Services.Implementation
{
    public class UserOrderService : IUserOrderService
    {
        private readonly IUserOrderRepository _userOrderRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public UserOrderService(
            IUserOrderRepository userOrderRepo,
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager)
        {
            _userOrderRepo = userOrderRepo;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Order>> UserOrders(bool getAll = false)
        {
            if (getAll)
            {
                return await _userOrderRepo.UserOrders(null, true);
            }

            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            var userId = _userManager.GetUserId(user);

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User not logged in");

            return await _userOrderRepo.UserOrders(userId, false);
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _userOrderRepo.GetOrderById(id);
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatuses()
        {
            return await _userOrderRepo.GetOrderStatuses();
        }

        public async Task ChangeOrderStatus(UpdateOrderStatusModel data)
        {
            // Business rule example (you can expand later)
            if (data.OrderId <= 0)
                throw new ArgumentException("Invalid Order Id");

            await _userOrderRepo.ChangeOrderStatus(data);
        }

        public async Task TogglePaymentStatus(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentException("Invalid Order Id");

            await _userOrderRepo.TogglePaymentStatus(orderId);
        }
    }
}
