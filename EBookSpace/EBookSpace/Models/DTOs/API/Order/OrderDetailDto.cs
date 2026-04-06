namespace EBookSpace.Models.DTOs.API.Order
{
    public class OrderDetailDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
