using Microsoft.AspNetCore.Identity;
using PizzaPanda_Store.Models;

namespace PizzaPanda_Restaurant.Models
{
    public class UserViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; } = null;
        public IEnumerable<IdentityRole> Roles { get; set; } = null;
    }
}
