using EBookSpace.Models.DTOs.API.Account;
using EBookSpace.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBookSpace.Controllers.API_s
{ 

    [Route("api/[Controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthAPIController(
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null)
                return Unauthorized("Invalid username");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid password");

            var token = _tokenService.CreateToken(user);

            return Ok(user.ToNewUserDto(token));
        }

      
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                Email = registerDto.Email
            };

            var createdUser = await _userManager
                .CreateAsync(appUser, registerDto.Password);

            if (!createdUser.Succeeded)
                return StatusCode(500, createdUser.Errors);

            var roleResult = await _userManager
                .AddToRoleAsync(appUser, "User");

            if (!roleResult.Succeeded)
                return StatusCode(500, roleResult.Errors);

            var token = _tokenService.CreateToken(appUser);

            return Ok(AccountMapper.ToNewUserDto(appUser,token));
        }
    }
}
