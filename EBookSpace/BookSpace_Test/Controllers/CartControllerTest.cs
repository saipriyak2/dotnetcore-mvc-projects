//using BookShoppingCartMvcUI.Models.DTOs;
//using BookShoppingCartUI.Controllers;
//using BookShoppingCartUI.Repositories;
//using BookShoppingCartUI.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Hosting;
//using Moq;
//using NuGet.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace BookSpace_Test.Controller_Tests
//{
//    public class CartControllerTest
//    {
//        private readonly Mock<ICartService> _cartServiceMock;
//        private readonly CartController _controller;

//        public CartControllerTest()
//        {
//            _cartServiceMock = new Mock<ICartService>();
//            _controller = new CartController(_cartServiceMock.Object);
//        }



//        //2. AddItem Tests
//        //Case 1: Return Ok when redirect = 0
//        [Fact]
//        public async Task AddItem_ReturnsOk_WhenRedirectIsZero()
//        {
//            _cartServiceMock.Setup(x => x.AddItem(1, 1)).ReturnsAsync(3);

//            var result = await _controller.AddItem(1, 1, 0);

//            var okResult = Assert.IsType<OkObjectResult>(result);
//            Assert.Equal(3, okResult.Value);

//        }
//        //Case 2: Redirect when redirect != 0
//           [Fact]
//        public async Task AddItem_Redirects_WhenRedirectIsOne()
//        {
//            _cartServiceMock.Setup(x => x.AddItem(1, 1)).ReturnsAsync(3);

//            var result = await _controller.AddItem(1, 1, 1);

//            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
//            Assert.Equal("GetUserCart", redirectResult.ActionName);
//        }
//        //3. RemoveItem Test
//            [Fact]
//        public async Task RemoveItem_Redirects_ToGetUserCart()
//        {
//            _cartServiceMock.Setup(x => x.RemoveItem(1)).ReturnsAsync(2);

//            var result = await _controller.RemoveItem(1);

//            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
//            Assert.Equal("GetUserCart", redirectResult.ActionName);
//        }
//        //4. GetUserCart Test
//            [Fact]
//        public async Task GetUserCart_ReturnsView_WithCart()
//        {
//            var cart = new object(); // replace with actual CartDto
//            //_cartRepoMock.Setup(x => x.GetUserCart()).ReturnsAsync(cart);

//            var result = await _controller.GetUserCart();

//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.Equal(cart, viewResult.Model);
//        }
//        //5. GetTotalItemInCart Test
//            [Fact]
//        public async Task GetTotalItemInCart_ReturnsOk()
//        {
//           // _cartRepoMock.Setup(x => x.GetCartItemCount()).ReturnsAsync(5);

//            var result = await _controller.GetTotalItemInCart();

//            var okResult = Assert.IsType<OkObjectResult>(result);
//            Assert.Equal(5, okResult.Value);
//        }

//        //6. Checkout(GET)
//            [Fact]
//        public void Checkout_Get_ReturnsView()
//        {
//            var result = _controller.Checkout();

//            Assert.IsType<ViewResult>(result);
//        }

//        //7. Checkout(POST)
////❌ Case 1: Invalid ModelState
//            [Fact]
//        public async Task Checkout_Post_ReturnsView_WhenModelInvalid()
//        {
//            _controller.ModelState.AddModelError("Error", "Invalid");

//            var model = new CheckoutModel();

//            var result = await _controller.Checkout(model);

//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.Equal(model, viewResult.Model);
//        }

//        //❌ Case 2: Checkout fails
//            [Fact]
//        public async Task Checkout_Post_RedirectsToFailure_WhenCheckoutFails()
//        {
//            var model = new CheckoutModel();
//            _cartServiceMock.Setup(x => x.Checkout(model)).ReturnsAsync(false);

//            var result = await _controller.Checkout(model);

//            var redirect = Assert.IsType<RedirectToActionResult>(result);
//            Assert.Equal("OrderFailure", redirect.ActionName);
//        }

//        //Case 3: Checkout success
//            [Fact]
//        public async Task Checkout_Post_RedirectsToSuccess_WhenCheckoutSucceeds()
//        {
//            var model = new CheckoutModel();
//            _cartServiceMock.Setup(x => x.Checkout(model)).ReturnsAsync(true);

//            var result = await _controller.Checkout(model);

//            var redirect = Assert.IsType<RedirectToActionResult>(result);
//            Assert.Equal("OrderSuccess", redirect.ActionName);
//        }

//        //8. OrderSuccess & OrderFailure
//            [Fact]
//         public void OrderSuccess_ReturnsView()
//        {
//            var result = _controller.OrderSuccess();
//            Assert.IsType<ViewResult>(result);
//        }

//        [Fact]
//        public void OrderFailure_ReturnsView()
//        {
//            var result = _controller.OrderFailure();
//            Assert.IsType<ViewResult>(result);
//        }

//    }
    
//}
////If you want to make this portfolio-level strong:

////Add Authorize testing (mock user identity)
////Add FluentAssertions
////Add Integration tests with WebApplicationFactory
////Add edge cases (qty = 0, invalid bookId)

////BookShoppingCart.Tests
////│
////├── Controllers
////│     ├── CartControllerTests.cs
////│     ├── AdminControllerTests.cs
////│
////├── Services
////│     ├── CartServiceTests.cs
////│     ├── UserOrderServiceTests.cs
////│     ├── AdminServiceTests.cs
////│
////├── Repositories
////│     ├── UserOrderRepositoryTests.cs
////│
////├── Integration
////│     ├── OrderFlowTests.cs
////│
////├── EdgeCases
////│     ├── CartEdgeCaseTests.cs
////│     ├── OrderEdgeCaseTests.cs






