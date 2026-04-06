using EBookSpace.Constants;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EBookSpace.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var usrMgr = service.GetService<UserManager<AppUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();

            // adding some roles to db

            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

            // create admin user

            var admin = new AppUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            } ;

            var userInDb = await usrMgr.FindByEmailAsync(admin.Email);
            if(userInDb is null)
            {
                await usrMgr.CreateAsync(admin, "Admin@123");
                await usrMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }



        }
    }
}
