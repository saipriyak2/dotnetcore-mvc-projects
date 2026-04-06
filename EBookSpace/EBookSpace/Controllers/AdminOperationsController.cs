using EBookSpace.Constants;
using EBookSpace.Models.DTOs.UI;
using EBookSpace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = nameof(Roles.Admin))]
public class AdminOperationsController : Controller
{
    private readonly IAdminService _adminService;

    public AdminOperationsController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<IActionResult> AllOrders()
    {
        var orders = await _adminService.GetAllOrdersAsync();
        return View(orders);
    }

    public async Task<IActionResult> TogglePaymentStatus(int orderId)
    {
        try
        {
            await _adminService.TogglePaymentStatusAsync(orderId);
        }
        catch
        {
            TempData["msg"] = "Something went wrong";
        }

        return RedirectToAction(nameof(AllOrders));
    }

    public async Task<IActionResult> UpdateOrderStatus(int orderId)
    {
        var data = await _adminService.GetOrderStatusModelAsync(orderId);
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusModel data)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                data = await _adminService.GetOrderStatusModelAsync(data.OrderId);
                return View(data);
            }

            await _adminService.UpdateOrderStatusAsync(data);
            TempData["msg"] = "Updated successfully";
        }
        catch
        {
            TempData["msg"] = "Something went wrong";
        }

        return RedirectToAction(nameof(UpdateOrderStatus), new { orderId = data.OrderId });
    }

    public IActionResult Dashboard()
    {
        return View();
    }
}