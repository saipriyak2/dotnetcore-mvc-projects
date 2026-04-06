using EBookSpace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class UserOrderController : Controller
{
    private readonly IUserOrderService _userOrderService;

    public UserOrderController(IUserOrderService userOrderService)
    {
        _userOrderService = userOrderService;
    }

    public async Task<IActionResult> UserOrders()
    {
        var orders = await _userOrderService.UserOrders();
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _userOrderService.GetOrderById(id);
        if (order == null)
            return NotFound();

        return View(order);
    }
}