using EBookSpace.Constants;
using EBookSpace.Helpers;
using EBookSpace.Models.DTOs.UI;
using EBookSpace.Services.Interfaces;
using EBookSpace.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize(Roles = nameof(Roles.Admin))]
public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IGenreRepository _genreRepo;

    public BookController(IBookService bookService, IGenreRepository genreRepo)
    {
        _bookService = bookService;
        _genreRepo = genreRepo;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAllBooksAsync(new QueryObject());
        return View(books);
    }

    public async Task<IActionResult> AddBook()
    {
        var genres = await _genreRepo.GetGenres();
        var genreSelectList = genres.Select(g => new SelectListItem
        {
            Text = g.GenreName,
            Value = g.Id.ToString()
        });
        return View(new BookDTO { GenreList = genreSelectList });
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(BookDTO bookDto)
    {
        try
        {
            if (!ModelState.IsValid) return View(bookDto);

            await _bookService.AddBookAsync(bookDto);
            TempData["successMessage"] = "Book is added successfully";
            return RedirectToAction(nameof(AddBook));
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(bookDto);
        }
    }

    public async Task<IActionResult> UpdateBook(int id)
    {
        var bookDto = await _bookService.GetBookForUpdateAsync(id);
        if (bookDto == null)
        {
            TempData["errorMessage"] = $"Book with id {id} not found";
            return RedirectToAction(nameof(Index));
        }
        return View(bookDto);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBook(BookDTO bookDto)
    {
        try
        {
            if (!ModelState.IsValid) return View(bookDto);

            await _bookService.UpdateBookAsync(bookDto);
            TempData["successMessage"] = "Book updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(bookDto);
        }
    }

    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            await _bookService.DeleteBookAsync(id);
            TempData["successMessage"] = "Book deleted successfully";
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }
}