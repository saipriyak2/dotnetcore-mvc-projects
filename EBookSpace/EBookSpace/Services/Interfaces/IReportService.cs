using EBookSpace.Models.DTOs.UI;

namespace EBookSpace.Services.Interfaces
{
    public interface IReportService
    {
        Task<TopNSoldBooksVm> GetTopNSellingBooksAsync(DateTime? startDate, DateTime? endDate);
    }
}
