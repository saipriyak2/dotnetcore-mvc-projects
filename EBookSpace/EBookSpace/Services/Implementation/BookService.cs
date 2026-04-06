using EBookSpace.Models.DTOs.UI;
using EBookSpace.Services.Interfaces;
using EBookSpace.Shared;
using EBookSpace.Helpers;
using EBookSpace.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EBookSpace.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IFileService _fileService;

        public BookService(IBookRepository bookRepo, IGenreRepository genreRepo, IFileService fileService)
        {
            _bookRepo = bookRepo;
            _genreRepo = genreRepo;
            _fileService = fileService;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(QueryObject query)
        {
            return await _bookRepo.GetAllAsync(query);
        }

        public async Task<BookDTO?> GetBookForUpdateAsync(int id)
        {
            var book = await _bookRepo.GetBookById(id);
            if (book == null) return null;

            var genres = await _genreRepo.GetGenres();
            var genreSelectList = genres.Select(g => new SelectListItem
            {
                Text = g.GenreName,
                Value = g.Id.ToString(),
                Selected = g.Id == book.GenreId
            });

            return new BookDTO
            {
                Id = book.Id,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                GenreId = book.GenreId,
                Price = book.Price,
                Image = book.Image,
                GenreList = genreSelectList
            };
        }

        public async Task AddBookAsync(BookDTO bookDto)
        {
            if (bookDto.ImageFile != null)
            {
                if (bookDto.ImageFile.Length > 1 * 1024 * 1024)
                    throw new InvalidOperationException("Image file can not exceed 1 MB");

                string[] allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
                bookDto.Image = await _fileService.SaveFile(bookDto.ImageFile, allowedExtensions);
            }

            var book = new Book
            {
                BookName = bookDto.BookName,
                AuthorName = bookDto.AuthorName,
                GenreId = bookDto.GenreId,
                Price = bookDto.Price,
                Image = bookDto.Image
            };

            await _bookRepo.AddBook(book);
        }

        public async Task UpdateBookAsync(BookDTO bookDto)
        {
            var oldBook = await _bookRepo.GetBookById(bookDto.Id);
            if (oldBook == null)
                throw new InvalidOperationException($"Book with id {bookDto.Id} not found");

            string oldImage = oldBook.Image;

            if (bookDto.ImageFile != null)
            {
                if (bookDto.ImageFile.Length > 1 * 1024 * 1024)
                    throw new InvalidOperationException("Image file can not exceed 1 MB");

                string[] allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
                bookDto.Image = await _fileService.SaveFile(bookDto.ImageFile, allowedExtensions);
            }

            oldBook.BookName = bookDto.BookName;
            oldBook.AuthorName = bookDto.AuthorName;
            oldBook.GenreId = bookDto.GenreId;
            oldBook.Price = bookDto.Price;
            oldBook.Image = bookDto.Image ?? oldBook.Image;

            await _bookRepo.UpdateBook(bookDto.Id, oldBook);

            // delete old image if updated
            if (!string.IsNullOrWhiteSpace(bookDto.ImageFile?.FileName) && !string.IsNullOrWhiteSpace(oldImage))
            {
                _fileService.DeleteFile(oldImage);
            }
        }
       

           
                

                
            

         
        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepo.GetBookById(id);
            if (book == null)
                throw new InvalidOperationException($"Book with id {id} not found");

            await _bookRepo.DeleteBook(id);

            if (!string.IsNullOrWhiteSpace(book.Image))
            {
                _fileService.DeleteFile(book.Image);
            }
        }
    }
}
