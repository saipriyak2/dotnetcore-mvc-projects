using EBookSpace.Models;
using EBookSpace.Models.DTOs;
using EBookSpace.Models.DTOs.API.Order;

namespace EBookSpace.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CreateDate = order.CreateDate,
                OrderStatusId = order.OrderStatusId,
                Name = order.Name,
                Email = order.Email!,
                MobileNumber = order.MobileNumber!,
                Address = order.Address!,
                PaymentMethod = order.PaymentMethod!,
                IsPaid = order.IsPaid,

                Items = order.OrderDetail.Select(od => new OrderDetailDto
                {
                    BookId = od.BookId,
                    BookTitle = od.Book.BookName,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                }).ToList()
            };
        }
    }
}
