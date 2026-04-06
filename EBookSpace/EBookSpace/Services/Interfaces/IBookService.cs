using EBookSpace.Models.DTOs.UI;
using EBookSpace.Helpers;

namespace EBookSpace.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(QueryObject query);
        Task<BookDTO?> GetBookForUpdateAsync(int id);
        Task AddBookAsync(BookDTO bookDto);
        Task UpdateBookAsync(BookDTO bookDto);
        Task DeleteBookAsync(int id);
    }
}
