using EBookSpace.Models.DTOs.UI;
using EBookSpace.Services.Interfaces;

namespace EBookSpace.Services.Implementation
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepo;

        public ReportService(IReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }

        public async Task<TopNSoldBooksVm> GetTopNSellingBooksAsync(DateTime? startDate, DateTime? endDate)
        {
            // Apply default dates if null
            DateTime fromDate = startDate ?? DateTime.UtcNow.AddDays(-7);
            DateTime toDate = endDate ?? DateTime.UtcNow;

            // Call repository
            var topBooks = await _reportRepo.GetTopNSoldBooksbyDate(fromDate, toDate);

            // Map to ViewModel
            var vm = new TopNSoldBooksVm(fromDate, toDate, topBooks);

            return vm;
        }
    }
}
