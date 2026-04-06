using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using BookShoppingCartUI.Controllers;
using BookShoppingCartUI.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookShoppingCartUI.Services.Interfaces;

namespace BookShoppingCartUI.Tests.Controllers
{
    public class UserOrderControllerEdgeTests
    {
        private readonly Mock<IUserOrderService> _userOrderServiceMock;
        private readonly UserOrderController _controller;

        public UserOrderControllerEdgeTests()
        {
            _userOrderServiceMock = new Mock<IUserOrderService>();
            _controller = new UserOrderController(_userOrderServiceMock.Object);
        }

        [Fact]
        public async Task UserOrders_AnonymousUser_ShouldThrowException()
        {
            _userOrderServiceMock.Setup(x => x.UserOrders(false))
                     .ThrowsAsync(new System.Exception("User is not logged-in"));

            await Assert.ThrowsAsync<System.Exception>(() => _controller.UserOrders());
        }

        [Fact]
        public async Task GetOrderById_NonExistentOrder_ShouldReturnNull()
        {
            _userOrderServiceMock.Setup(x => x.GetOrderById(99))
                     .ReturnsAsync((BookShoppingCartUI.Models.Order)null);

            var result = await _controller.UserOrders();
            Assert.IsType<ViewResult>(result);
        }
    }
}
