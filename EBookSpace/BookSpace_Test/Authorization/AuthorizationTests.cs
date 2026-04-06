using Xunit;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using BookShoppingCartUI.Controllers;

public class AuthorizationTests
{
    [Fact]
    public void UserOrderController_ShouldHaveAuthorizeAttribute()
    {
        // Arrange
        var type = typeof(UserOrderController);

        // Act
        var authorizeAttr = type.GetCustomAttributes(typeof(AuthorizeAttribute), true)
                                .Cast<AuthorizeAttribute>()
                                .FirstOrDefault();

        // Assert
        Assert.NotNull(authorizeAttr); // The controller has [Authorize]
        Assert.True(string.IsNullOrEmpty(authorizeAttr.Roles)); // Roles not required for normal users
    }

    [Fact]
    public void AdminOperationsController_ShouldHaveAdminRole()
    {
        var type = typeof(AdminOperationsController);

        var authorizeAttr = type.GetCustomAttributes(typeof(AuthorizeAttribute), true)
                                .Cast<AuthorizeAttribute>()
                                .FirstOrDefault();

        Assert.NotNull(authorizeAttr);
        Assert.Equal("Admin", authorizeAttr.Roles); // Must be Admin
    }

    [Fact]
    public void UserOrders_Action_ShouldHaveAuthorize()
    {
        var method = typeof(UserOrderController).GetMethod("UserOrders");
        var authorizeAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), true)
                                  .Cast<AuthorizeAttribute>()
                                  .FirstOrDefault();

        Assert.NotNull(authorizeAttr);
    }
}