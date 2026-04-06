using EBookSpace.Models.DTOs.UI;
using EBookSpace.Services.Interfaces;

namespace EBookSpace.Services.Implementation
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepo;

        public StockService(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocksAsync(string searchTerm = "")
        {
            return await _stockRepo.GetStocks(searchTerm);
        }

        public async Task<StockDTO> GetStockForBookAsync(int bookId)
        {
            var existingStock = await _stockRepo.GetStockByBookId(bookId);
            return new StockDTO
            {
                BookId = bookId,
                Quantity = existingStock?.Quantity ?? 0
            };
        }

        public async Task ManageStockAsync(StockDTO stockDto)
        {
            if (stockDto.Quantity < 0)
                throw new InvalidOperationException("Stock quantity cannot be negative");

            await _stockRepo.ManageStock(stockDto);
        }
    }
}
