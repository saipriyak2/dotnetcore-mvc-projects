using EBookSpace.Models.DTOs.UI;

namespace EBookSpace.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task TogglePaymentStatusAsync(int orderId);
        Task<UpdateOrderStatusModel> GetOrderStatusModelAsync(int orderId);
        Task UpdateOrderStatusAsync(UpdateOrderStatusModel data);
    }
}
