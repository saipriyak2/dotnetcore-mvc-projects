using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// username: saipriya.k50@gmail.com pwd: Saipriya@123
// links used Bootstrap 5 Modal
namespace EBookSpace.Models
{
    [Table("Book")]
    public class Book
    {
       
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? BookName { get; set; }
        [Required]
        [MaxLength(40)]
        public string? AuthorName { get; set; }

        [Required]
       
        public double Price { get; set; }
        //Its primary use is to explicitly declare the programmer's intent that the Image property is allowed 
        //to have a value of null [1]. This is a feature introduced in C# 8.0 to help minimize the risk of 
        //NullReferenceException errors
        public string? Image { get; set; }
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        //public Order Order { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        public Stock Stock { get; set; }
        [NotMapped]
        public string GenreName { get; set; }
        [NotMapped]
        public int Quantity { get; set; }
    }
}
