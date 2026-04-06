namespace EBookSpace.Models.DTOs.API.Cart
{
    public class AddCartItemDTO
    {
        public int BookId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
