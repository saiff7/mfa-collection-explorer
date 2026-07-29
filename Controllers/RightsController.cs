using Microsoft.AspNetCore.Mvc;

namespace MfaCollectionExplorer.Controllers;

public class RightsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}