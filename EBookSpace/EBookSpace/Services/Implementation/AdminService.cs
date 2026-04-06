using EBookSpace.Models.DTOs.UI;
using EBookSpace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EBookSpace.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IUserOrderRepository _repo;

        public AdminService(IUserOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _repo.UserOrders(null, true);
        }

        public async Task TogglePaymentStatusAsync(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentException("Invalid order id");

            await _repo.TogglePaymentStatus(orderId);
        }

        public async Task<UpdateOrderStatusModel> GetOrderStatusModelAsync(int orderId)
        {
            var order = await _repo.GetOrderById(orderId);

            if (order == null)
                throw new InvalidOperationException($"Order with id:{orderId} not found");

            var statuses = await _repo.GetOrderStatuses();

            return new UpdateOrderStatusModel
            {
                OrderId = orderId,
                OrderStatusId = order.OrderStatusId,
                OrderStatusList = statuses.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StatusName,
                    Selected = s.Id == order.OrderStatusId
                })
            };
        }

        public async Task UpdateOrderStatusAsync(UpdateOrderStatusModel data)
        {
            if (data.OrderId <= 0)
                throw new ArgumentException("Invalid order id");

            await _repo.ChangeOrderStatus(data);
        }
    }
}
