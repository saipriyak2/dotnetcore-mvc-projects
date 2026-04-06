namespace EBookSpace.Models.DTOs.API.AdminOrder
{
    public class AdminOrderItemDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
