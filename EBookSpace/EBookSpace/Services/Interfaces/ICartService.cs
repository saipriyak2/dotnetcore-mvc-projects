using EBookSpace.Models.DTOs.UI;

namespace EBookSpace.Services.Interfaces
{
    public interface ICartService
    {
        Task<int> AddItem(int bookId, int qty);
        Task<int> RemoveItem(int bookId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount();
        Task<bool> Checkout(CheckoutModel model);
    }
}
