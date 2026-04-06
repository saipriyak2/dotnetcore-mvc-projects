using EBookSpace.Constants;
using EBookSpace.Models.DTOs.UI;
using EBookSpace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = nameof(Roles.Admin))]
public class StockController : Controller
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    public async Task<IActionResult> Index(string sTerm = "")
    {
        var stocks = await _stockService.GetStocksAsync(sTerm);
        return View(stocks);
    }

    public async Task<IActionResult> ManageStock(int bookId)
    {
        var stockDto = await _stockService.GetStockForBookAsync(bookId);
        return View(stockDto);
    }

    [HttpPost]
    public async Task<IActionResult> ManageStock(StockDTO stockDto)
    {
        if (!ModelState.IsValid)
            return View(stockDto);

        try
        {
            await _stockService.ManageStockAsync(stockDto);
            TempData["successMessage"] = "Stock updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(stockDto);
        }
    }
}