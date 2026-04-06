//using Xunit;
//using Moq;
//using Microsoft.AspNetCore.Mvc;
//using BookShoppingCartUI.Controllers;
//using BookShoppingCartUI.Repositories;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using BookShoppingCartUI.Services.Interfaces;

//namespace BookShoppingCartUI.Tests.Controllers
//{
//    public class AdminOperationsControllerEdgeCaseTests
//    {
//        private readonly Mock<IUserOrderService> _userOrderServiceMock;
//        private readonly AdminOperationsController _controller;

//        public AdminOperationsControllerEdgeCaseTests()
//        {
//            _userOrderServiceMock = new Mock<IUserOrderService>();
//            _controller = new AdminOperationsController(_userOrderServiceMock.Object);
//        }

//        [Fact]
//        public async Task TogglePaymentStatus_InvalidOrderId_ShouldNotCrash()
//        {
//            _repoMock.Setup(x => x.TogglePaymentStatus(0))
//                     .ThrowsAsync(new System.Exception("Invalid order id"));

//            await Assert.ThrowsAsync<System.Exception>(() => _controller.TogglePaymentStatus(0));
//        }

//        [Fact]
//        public async Task UpdateOrderStatus_NonExistentOrder_ShouldThrowException()
//        {
//            _repoMock.Setup(x => x.GetOrderById(99))
//                     .ReturnsAsync((BookShoppingCartUI.Models.Order)null);

//            await Assert.ThrowsAsync<System.InvalidOperationException>(() => _controller.UpdateOrderStatus(99));
//        }

//        [Fact]
//        public async Task UpdateOrderStatus_Post_InvalidModel_ShouldReturnView()
//        {
//            var data = new BookShoppingCartUI.Models.DTOs.UpdateOrderStatusModel();
//            _controller.ModelState.AddModelError("OrderStatusId", "Required");

//            _repoMock.Setup(x => x.GetOrderStatuses())
//                     .ReturnsAsync(new List<BookShoppingCartUI.Models.OrderStatus>());

//            var result = await _controller.UpdateOrderStatus(data);

//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.NotNull(viewResult.Model);
//        }
//    }
//}
