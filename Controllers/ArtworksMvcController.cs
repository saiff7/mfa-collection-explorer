using Microsoft.AspNetCore.Mvc;
using MfaCollectionExplorer.Services;

namespace MfaCollectionExplorer.Controllers;

// This is the MVC controller for Artwork VIEWS (not the API).
// The API lives in ArtworksController.cs (ApiController).
// Routes: /artworks and /artworks/{id}
[Route("artworks")]
public class ArtworksMvcController : Controller
{
    private readonly IArtworkService _service;

    public ArtworksMvcController(IArtworkService service)
    {
        _service = service;
    }

    // GET /artworks  →  Views/Artworks/Index.cshtml
    [HttpGet("")]
    public IActionResult Index()
    {
        return View("~/Views/Artworks/Index.cshtml");
    }

    // GET /artworks/{id}  →  Views/Artworks/Detail.cshtml
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var artwork = await _service.GetArtworkByIdAsync(id);
        if (artwork == null) return NotFound();
        return View("~/Views/Artworks/Detail.cshtml", artwork);
    }
}