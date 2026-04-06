using EBookSpace.Models.DTOs.UI;
using EBookSpace.Services.Interfaces;
using EBookSpace.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EBookSpace.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpcontextaccessor;
        //private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartRepo, ApplicationDbContext db, ILogger<CartService> logger, IHttpContextAccessor httpcontextaccessor, UserManager<AppUser> userManager)
        {
            _cartRepo = cartRepo;
            _db = db;
            _userManager = userManager;
            _httpcontextaccessor = httpcontextaccessor;
            //_logger = logger;
        }

        public async Task<int> AddItem(int bookId, int qty)
        {
            if (bookId <= 0 || qty <= 0)
                throw new ArgumentException("Invalid input");

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _cartRepo.AddItem(bookId, qty);
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                //_logger.LogError(ex, "Error adding item to cart");
                throw;
            }
        }

        public async Task<int> RemoveItem(int bookId)
        {
            if (bookId <= 0)
                throw new ArgumentException("Invalid bookId");

            return await _cartRepo.RemoveItem(bookId);
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            return await _cartRepo.GetUserCart();
        }

        public async Task<int> GetCartItemCount()
        {
            return await _cartRepo.GetCartItemCount();
        }

        public async Task<bool> Checkout(CheckoutModel model)
        {
            var userId = _userManager.GetUserId(_httpcontextaccessor.HttpContext.User);

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User not logged in");

            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var result = await _cartRepo.Checkout(userId, model);

                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                //_logger.LogError(ex, "Checkout failed");
                throw; // 🔥 IMPORTANT (don’t return false silently)
            }
        }
    }
}
