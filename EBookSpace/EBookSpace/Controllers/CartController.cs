using EBookSpace.Models.DTOs.UI;
using EBookSpace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBookSpace.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> AddItem(int bookId, int qty = 1, int redirect = 0)
        {
            try
            {
                var cartCount = await _cartService.AddItem(bookId, qty);

                if (redirect == 0)
                    return Ok(cartCount);

                return RedirectToAction(nameof(GetUserCart));
            }
            catch (Exception ex)
            {
                // log the exception here if needed
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> RemoveItem(int bookId)
        {
            try
            {
                var cartCount = await _cartService.RemoveItem(bookId);
                return RedirectToAction(nameof(GetUserCart));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetUserCart()
        {
            try
            {
                var cart = await _cartService.GetUserCart();
                return View(cart);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            try
            {
                int cartItem = await _cartService.GetCartItemCount();
                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _cartService.Checkout(model);
                return RedirectToAction(nameof(OrderSuccess));
            }
            catch (Exception ex)
            {
                // log error
                return RedirectToAction(nameof(OrderFailure));
            }
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }
    }
}