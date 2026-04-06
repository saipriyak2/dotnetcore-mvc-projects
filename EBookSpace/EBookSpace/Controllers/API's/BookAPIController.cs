using EBookSpace.Mappers;
using EBookSpace.Models.DTOs.API.BookDTO;
using EBookSpace.Services.Interfaces;
using EBookSpace.Helpers;
using EBookSpace.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBookSpace.Controllers.API_s
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BookApiController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IBookRepository _bookrepo;

        public BookApiController(IBookService service, IBookRepository bookrepo)
        {
            _service = service;
            _bookrepo = bookrepo;
        }

       [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var books = await _service.GetAllBooksAsync(query);
            var bookDto = books.Select(s => s.ToBookDto()).ToList();
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = await _bookrepo.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequestDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookModel = bookDto.ToBookFromCreateDto();
            await _bookrepo.AddBook(bookModel);
            return CreatedAtAction(nameof(GetById), new { id = bookModel.Id }, bookModel.ToBookDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Fetch existing entity
            var existingBook = await _bookrepo.GetBookById(id);
            if (existingBook == null)
                return NotFound();

            // Map DTO to entity (using mapper)
            existingBook.UpdateBookFromDto(updateDto); // this updates the entity in memory

            // Call repository
            await _bookrepo.UpdateBook( id,existingBook);

            // Return updated DTO
            return Ok(existingBook.ToBookDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _bookrepo.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookrepo.DeleteBook(id);

            return NoContent();
        }



    }
}
