using Microsoft.EntityFrameworkCore;
using EBookSpace.Helpers;

namespace EBookSpace.Repositories
{
    public interface IBookRepository
    {
        Task AddBook(Book book);
        Task<Book?> DeleteBook(int id);
        Task<Book?> GetBookById(int id);
        //Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<Book>> GetAllAsync(QueryObject query);
        Task UpdateBook(int id,Book book);
    }
    public class BookRepository: IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(int id,Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book?> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return null;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Book?> GetBookById(int id) => await _context.Books.FindAsync(id);

        //public async Task<IEnumerable<Book>> GetBooks() => await _context.Books.Include(a => a.Genre).ToListAsync();
        public async Task<IEnumerable<Book>> GetAllAsync(QueryObject query)
        {
            var books = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Title))
                books = books.Where(b => b.BookName.Contains(query.Title));

            if (!string.IsNullOrWhiteSpace(query.AuthorName))
                books = books.Where(b => b.AuthorName.Contains(query.AuthorName));

            // Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
                books = query.IsDescending
                    ? books.OrderByDescending(b => EF.Property<object>(b, query.SortBy))
                    : books.OrderBy(b => EF.Property<object>(b, query.SortBy));

            // Pagination
            books = books.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);

            return await books.ToListAsync();
        }
    }
}
