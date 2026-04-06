using EBookSpace.Models.DTOs.API.BookDTO;

namespace EBookSpace.Mappers
{
    public static class BookMapper
    {
        // 🔹 Entity → Response DTO
        public static BookDto ToBookDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                GenreId = book.GenreId,
                Price = book.Price,
                Image = book.Image
            };
        }

        // 🔹 Create DTO → Entity
        public static Book ToBookFromCreateDto(this CreateBookRequestDto dto)
        {
            return new Book
            {
                BookName = dto.BookName,
                AuthorName = dto.AuthorName,
                GenreId = dto.GenreId,
                Price = dto.Price
                // Image usually handled separately (file upload)
            };
        }

        // 🔹 Update DTO → Existing Entity
        public static void UpdateBookFromDto(this Book book, UpdateBookRequestDto dto)
        {
            book.BookName = dto.BookName;
            book.AuthorName = dto.AuthorName;
            book.GenreId = dto.GenreId;
            book.Price = dto.Price;
        }
    }
}
