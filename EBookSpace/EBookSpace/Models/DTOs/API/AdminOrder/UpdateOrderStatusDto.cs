namespace EBookSpace.Models.DTOs.API.AdminOrder
{
    public class UpdateOrderStatusDto
    {
        public int OrderStatusId { get; set; }
        public bool IsPaid { get; set; }
    }
}
