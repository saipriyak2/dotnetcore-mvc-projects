using Microsoft.AspNetCore.Identity;

namespace EBookSpace.Models;

// AppUser must inherit from IdentityUser
public class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public string? Address { get; set; }

    public ICollection<Order>? Orders { get; set; }
}
