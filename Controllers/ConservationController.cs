using Microsoft.AspNetCore.Mvc;

namespace MfaCollectionExplorer.Controllers;

public class ConservationController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}