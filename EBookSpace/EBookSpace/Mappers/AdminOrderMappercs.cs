using EBookSpace.Models;
using EBookSpace.Models.DTOs.API.AdminOrder;

namespace EBookSpace.Mappers
{
    public static class AdminOrderMapper
    {
        public static AdminOrderDto ToAdminOrderDto(this Order order)
        {
            return new AdminOrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreateDate = order.CreateDate,
                OrderStatusId = order.OrderStatusId,
                Name = order.Name,
                Email = order.Email!,
                MobileNumber = order.MobileNumber!,
                Address = order.Address!,
                PaymentMethod = order.PaymentMethod!,
                IsPaid = order.IsPaid,

                Items = order.OrderDetail.Select(od => new AdminOrderItemDto
                {
                    BookId = od.BookId,
                    BookName = od.Book.BookName,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                }).ToList()
            };
        }
    }
}
