using Microsoft.AspNetCore.Mvc;
using MfaCollectionExplorer.Models.DTOs;
using MfaCollectionExplorer.Services;

namespace MfaCollectionExplorer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtworksController : ControllerBase
{
    private readonly IArtworkService _service;

    public ArtworksController(IArtworkService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetArtworks(
        [FromQuery] string department = "All",
        [FromQuery] int page = 1,
        [FromQuery] string search = "")
    {
        var result = await _service.GetArtworksAsync(department, page, search);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArtwork(int id)
    {
        var artwork = await _service.GetArtworkByIdAsync(id);
        if (artwork == null) return NotFound();
        return Ok(artwork);
    }

    [HttpPost]
    public async Task<IActionResult> CreateArtwork([FromBody] ArtworkDto dto)
    {
        var id = await _service.CreateArtworkAsync(dto);
        return CreatedAtAction(nameof(GetArtwork), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArtwork(int id, [FromBody] ArtworkDto dto)
    {
        var success = await _service.UpdateArtworkAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArtwork(int id)
    {
        var success = await _service.DeleteArtworkAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet("{id}/conservation")]
    public async Task<IActionResult> GetConservationLogs(int id)
    {
        var logs = await _service.GetConservationLogsAsync(id);
        return Ok(logs);
    }

    [HttpPost("{id}/conservation")]
    public async Task<IActionResult> CreateConservationLog(int id, [FromBody] ConservationLogDto dto)
    {
        dto.ArtworkId = id;
        var logId = await _service.CreateConservationLogAsync(dto);
        return CreatedAtAction(nameof(GetConservationLogs), new { id }, new { logId });
    }

    [HttpGet("{id}/rights")]
    public async Task<IActionResult> GetRights(int id)
    {
        var rights = await _service.GetRightsAsync(id);
        return Ok(rights);
    }

    [HttpPost("{id}/rights")]
    public async Task<IActionResult> CreateRights(int id, [FromBody] RightsLicensingDto dto)
    {
        dto.ArtworkId = id;
        var rightsId = await _service.CreateRightsAsync(dto);
        return CreatedAtAction(nameof(GetRights), new { id }, new { rightsId });
    }
}