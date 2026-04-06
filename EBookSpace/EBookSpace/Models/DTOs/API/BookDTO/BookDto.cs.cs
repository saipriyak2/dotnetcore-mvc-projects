namespace EBookSpace.Models.DTOs.API.BookDTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int GenreId { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
    }
}
