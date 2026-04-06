using Microsoft.AspNetCore.Identity;
using PizzaPanda_Store.Models;

namespace PizzaPanda_Restaurant.Models
{
    public class IdentityConfig
    {
        public static async Task CreateAdminUserAsync(IServiceProvider provider)
        {
           var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
           var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();

            string username = "admin@pizzapanda.com";
            string password = "Admin@123";
            string roleName = "Admin";
            // if role doesn't exist create it.
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // if username doesn't exist create it and add to role.
            if (await userManager.FindByNameAsync(username) == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = username,
                    Email = "admin@pizzapanda.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
