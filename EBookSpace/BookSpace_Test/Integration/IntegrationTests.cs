//using Microsoft.AspNetCore.Mvc.Testing;
//using System.Net;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;
//using Xunit;

//namespace BookShoppingCartUI.Tests.Integration
//{
//    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
//    {
//        private readonly WebApplicationFactory<Program> _factory;

//        public AuthIntegrationTests(WebApplicationFactory<Program> factory)
//        {
//            _factory = factory;
//        }

//        [Fact]
//        public async Task AdminController_AnonymousUser_ShouldRedirectToLogin()
//        {
//            var client = _factory.CreateClient();

//            var response = await client.GetAsync("/AdminOperations/AllOrders");

//            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
//            Assert.Contains("/Identity/Account/Login", response.Headers.Location.OriginalString);
//        }

//        [Fact]
//        public async Task AdminController_NonAdminUser_ShouldReturnForbidden()
//        {
//            var client = _factory.CreateClient();

//            // Simulate normal user login
//            client.DefaultRequestHeaders.Authorization =
//                new AuthenticationHeaderValue("TestScheme", "NormalUser");

//            var response = await client.GetAsync("/AdminOperations/AllOrders");

//            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
//        }

//        [Fact]
//        public async Task AdminController_AdminUser_ShouldAllowAccess()
//        {
//            var client = _factory.CreateClient();

//            // Simulate admin login
//            client.DefaultRequestHeaders.Authorization =
//                new AuthenticationHeaderValue("TestScheme", "AdminUser");

//            var response = await client.GetAsync("/AdminOperations/AllOrders");

//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//        }
//    }
//}
//In program.cs
//using Microsoft.AspNetCore.Authentication;

//builder.Services.AddAuthentication("TestScheme")
//    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });
