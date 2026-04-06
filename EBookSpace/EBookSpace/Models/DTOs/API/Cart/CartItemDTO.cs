namespace EBookSpace.Models.DTOs.API.Cart
{
    public class CartItemDTO
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
