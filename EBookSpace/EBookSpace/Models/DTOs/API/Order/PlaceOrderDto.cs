namespace EBookSpace.Models.DTOs.API.Order
{
    public class PlaceOrderDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
    }
}
