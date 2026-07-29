using Microsoft.AspNetCore.Mvc;

namespace MfaCollectionExplorer.Controllers;

public class ReportsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}