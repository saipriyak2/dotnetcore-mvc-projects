using EBookSpace.Models.DTOs.UI;

namespace EBookSpace.Services.Interfaces
{
    public interface IStockService
    {
        Task<IEnumerable<StockDisplayModel>> GetStocksAsync(string searchTerm = "");
        Task<StockDTO> GetStockForBookAsync(int bookId);
        Task ManageStockAsync(StockDTO stockDto);
    }
}
