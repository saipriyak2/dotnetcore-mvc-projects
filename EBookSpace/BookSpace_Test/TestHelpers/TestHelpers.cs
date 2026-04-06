using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Security.Claims;

namespace BookShoppingCartUI.Tests.Helpers
{
    public static class TestHelpers
    {
        public static UserManager<IdentityUser> MockUserManager()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            var mgr = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns((ClaimsPrincipal p) =>
            {
                return p?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "test-user-id";
            });
            return mgr.Object;
        }

        public static HttpContext MockHttpContext(string userId = "test-user-id", bool isAdmin = false)
        {
            var context = new DefaultHttpContext();
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
            if (isAdmin)
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

            context.User = new ClaimsPrincipal(identity);
            return context;
        }
    }
}
