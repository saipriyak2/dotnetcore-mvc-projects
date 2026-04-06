using EBookSpace.Models.DTOs.UI;

namespace EBookSpace.Services.Interfaces
{
    public interface IUserOrderService
    {
        Task<IEnumerable<Order>> UserOrders(bool getAll = false);
        Task<Order?> GetOrderById(int id);
        Task<IEnumerable<OrderStatus>> GetOrderStatuses();
        Task ChangeOrderStatus(UpdateOrderStatusModel data);
        Task TogglePaymentStatus(int orderId);
    }
}
