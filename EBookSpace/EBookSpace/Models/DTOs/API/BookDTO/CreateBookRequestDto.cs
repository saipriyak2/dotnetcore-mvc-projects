using System.ComponentModel.DataAnnotations;

namespace EBookSpace.Models.DTOs.API.BookDTO
{
    public class CreateBookRequestDto
    {
        [Required]
        public string BookName { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
