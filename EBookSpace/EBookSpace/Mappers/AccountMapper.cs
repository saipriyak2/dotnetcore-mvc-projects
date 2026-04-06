using EBookSpace.Models.DTOs.API.Account;

namespace EBookSpace.Mappers
{
    public static class AccountMapper
    {
        public static NewUserDTO ToNewUserDto(this AppUser user, string token)
        {
            return new NewUserDTO
            {
                UserName = user.UserName!,
                Email = user.Email!,
                Token = token
            };
        }
    }
}
