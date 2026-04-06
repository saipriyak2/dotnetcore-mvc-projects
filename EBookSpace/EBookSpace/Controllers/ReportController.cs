using EBookSpace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class ReportController : Controller
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<IActionResult> TopFiveSellingBooks(DateTime? sDate = null, DateTime? eDate = null)
    {
        try
        {
            var vm = await _reportService.GetTopNSellingBooksAsync(sDate, eDate);
            return View(vm);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Something went wrong";
            return RedirectToAction("Index", "Home");
        }
    }

    public IActionResult Index()
    {
        return View();
    }
}