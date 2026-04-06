using System.ComponentModel.DataAnnotations;

namespace EBookSpace.Models.DTOs.API.Account
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
