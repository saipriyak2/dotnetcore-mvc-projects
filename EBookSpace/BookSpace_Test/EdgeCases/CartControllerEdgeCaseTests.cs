//using BookShoppingCartUI.Controllers;
//using BookShoppingCartUI.Repositories;
//using BookShoppingCartUI.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Threading.Tasks;
//using Xunit;

//namespace BookShoppingCartUI.Tests.Controllers
//{
//    public class CartControllerEdgeCaseTests
//    {
//        private readonly Mock<ICartService> _cartServiceMock;
//        private readonly CartController _controller;

//        public CartControllerEdgeCaseTests()
//        {
//            _cartServiceMock = new Mock<ICartService>();
//            _controller = new CartController(_cartServiceMock.Object);
//        }

//        [Fact]
//        public async Task AddItem_InvalidBookId_ShouldThrowException()
//        {
//            _cartServiceMock.Setup(x => x.AddItem(It.IsAny<int>(), It.IsAny<int>()))
//                         .ThrowsAsync(new System.Exception("Invalid book id"));

//            await Assert.ThrowsAsync<System.Exception>(() => _controller.AddItem(-1, 1));
//        }

//        [Fact]
//        public async Task RemoveItem_NotInCart_ShouldThrowException()
//        {
//            _cartServiceMock.Setup(x => x.RemoveItem(It.IsAny<int>()))
//                         .ThrowsAsync(new System.Exception("Item not in cart"));

//            await Assert.ThrowsAsync<System.Exception>(() => _controller.RemoveItem(99));
//        }
//    }
//}
