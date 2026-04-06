using EBookSpace.Models.DTOs.UI;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EBookSpace.Repositories
{
    public class ReportRepository:IReportRepository
    {
        private readonly ApplicationDbContext _context;


        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TopNSoldBookModel>> GetTopNSoldBooksbyDate(DateTime startDate, DateTime endDate)
        {
            var startDateParam = new SqlParameter("@StartDate", startDate);
            var endDateParam = new SqlParameter("@EndDate", endDate);
            var topFiveSoldBooks = await _context.Database.SqlQueryRaw<TopNSoldBookModel>(
                "exec usp_GetTopNSellingBooksByDate @StartDate, @EndDate", startDateParam, endDateParam
            ).ToListAsync();
            return topFiveSoldBooks;
        }
    }

    public interface IReportRepository
    {
        Task<IEnumerable<TopNSoldBookModel>> GetTopNSoldBooksbyDate(DateTime startDate, DateTime endDate);
    }
}
