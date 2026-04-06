namespace EBookSpace.Models.DTOs.API.AdminOrder
{
    public class AdminOrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public DateTime CreateDate { get; set; }
        public int OrderStatusId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }

        public string PaymentMethod { get; set; }
        public bool IsPaid { get; set; }

        public List<AdminOrderItemDto> Items { get; set; }
    }
}
