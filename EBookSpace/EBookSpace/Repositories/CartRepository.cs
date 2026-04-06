using EBookSpace.Models.DTOs.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EBookSpace.Repositories
{
    public class CartRepository: ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpcontextaccessor;
        public CartRepository(ApplicationDbContext db, IHttpContextAccessor httpcontextaccessor, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _httpcontextaccessor = httpcontextaccessor;
        }


        //Adds a book to the logged-in user’s shopping cart.
        //If the cart doesn’t exist → create it.
        //If the book already exists in the cart → increase quantity.
        //Otherwise → add a new cart item.
        //Everything happens inside a database transaction.
        //Finally, it returns total cart item count.
        public async Task<int> AddItem(int bookId, int qty)
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException();

            var cart = await GetCart(userId);

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                await _db.ShoppingCarts.AddAsync(cart);
                await _db.SaveChangesAsync();
            }

            var cartItem = await _db.CartDetails
                .FirstOrDefaultAsync(x => x.ShoppingCartId == cart.Id && x.BookId == bookId);

            if (cartItem != null)
            {
                cartItem.Quantity += qty;
            }
            else
            {
                var book = await _db.Books.FindAsync(bookId);
                if (book == null)
                    throw new Exception("Book not found");

                cartItem = new CartDetail
                {
                    BookId = bookId,
                    ShoppingCartId = cart.Id,
                    Quantity = qty,
                    UnitPrice = book.Price
                };

                await _db.CartDetails.AddAsync(cartItem);
            }

            await _db.SaveChangesAsync();

            return await GetCartItemCount(userId);
        }

        public async Task<int> RemoveItem(int bookId)
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not logged in");

            var cart = await GetCart(userId);
            if (cart is null)
                throw new InvalidOperationException("Invalid cart");

            var cartItem = await _db.CartDetails
                .FirstOrDefaultAsync(x => x.ShoppingCartId == cart.Id && x.BookId == bookId);

            if (cartItem is null)
                throw new InvalidOperationException("Item not found in cart");

            if (cartItem.Quantity == 1)
            {
                _db.CartDetails.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity -= 1;
            }

            await _db.SaveChangesAsync();

            return await GetCartItemCount(userId);
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not logged in");

            var shoppingCart = await _db.ShoppingCarts
                .Where(c => c.UserId == userId)
                .Include(c => c.CartDetails)
                    .ThenInclude(cd => cd.Book)
                        .ThenInclude(b => b.Stock)
                .Include(c => c.CartDetails)
                    .ThenInclude(cd => cd.Book)
                        .ThenInclude(b => b.Genre)
                .AsNoTracking() // important for read-only
                .FirstOrDefaultAsync();

            return shoppingCart;
        }
        public async Task<ShoppingCart> GetCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("Invalid userId");

            return await _db.ShoppingCarts
                .AsTracking() // explicit: we may modify cart later
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<int> GetCartItemCount(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("Invalid userId");

            return await (from cart in _db.ShoppingCarts
                          join cartDetail in _db.CartDetails
                          on cart.Id equals cartDetail.ShoppingCartId
                          where cart.UserId == userId
                          select cartDetail.Id)
                         .CountAsync();
        }
        public async Task<bool> Checkout(string userId, CheckoutModel model)
        {
            var cart = await GetCart(userId);
            if (cart == null)
                throw new InvalidOperationException("Invalid cart");

            var cartItems = await _db.CartDetails
                .Where(c => c.ShoppingCartId == cart.Id)
                .ToListAsync();

            if (!cartItems.Any())
                throw new InvalidOperationException("Cart is empty");

            var pendingStatus = await _db.OrderStatuses
                .FirstOrDefaultAsync(s => s.StatusName == "Pending");

            if (pendingStatus == null)
                throw new InvalidOperationException("Pending status not found");

            var order = new Order
            {
                UserId = userId,
                CreateDate = DateTime.UtcNow,
                Name = model.Name,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                PaymentMethod = model.PaymentMethod,
                Address = model.Address,
                IsPaid = false,
                OrderStatusId = pendingStatus.Id
            };

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            // Load all stocks at once (IMPORTANT)
            var bookIds = cartItems.Select(x => x.BookId).ToList();

            var stocks = await _db.Stocks
                .Where(s => bookIds.Contains(s.BookId))
                .ToListAsync();

            foreach (var item in cartItems)
            {
                var stock = stocks.FirstOrDefault(s => s.BookId == item.BookId);

                if (stock == null)
                    throw new InvalidOperationException("Stock not found");

                if (item.Quantity > stock.Quantity)
                    throw new InvalidOperationException($"Only {stock.Quantity} available");

                stock.Quantity -= item.Quantity;

                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };

                await _db.OrderDetails.AddAsync(orderDetail);
            }

            _db.CartDetails.RemoveRange(cartItems);

            await _db.SaveChangesAsync();

            return true;
        }

        private string GetUserId()
        {
            var principal = _httpcontextaccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;

            
        }
        
    }
}
