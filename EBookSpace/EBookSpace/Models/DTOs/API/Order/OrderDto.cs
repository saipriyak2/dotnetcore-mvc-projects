namespace EBookSpace.Models.DTOs.API.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int OrderStatusId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsPaid { get; set; }

        public List<OrderDetailDto> Items { get; set; }
    }
}
