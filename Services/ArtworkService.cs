using MfaCollectionExplorer.Models;
using MfaCollectionExplorer.Models.DTOs;
using MfaCollectionExplorer.Repositories;

namespace MfaCollectionExplorer.Services;

public class ArtworkService : IArtworkService
{
    private readonly IArtworkRepository _repo;
    private const int PageSize = 20;

    public ArtworkService(IArtworkRepository repo)
    {
        _repo = repo;
    }

    public async Task<PagedResult<Artwork>> GetArtworksAsync(string department, int page, string search)
    {
        var items = await _repo.GetByDepartmentAsync(department, page, PageSize);
        var list = items.ToList();

        if (!string.IsNullOrWhiteSpace(search))
            list = list.Where(a => a.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                                || (a.Artist?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)).ToList();

        var total = list.FirstOrDefault()?.TotalCount ?? 0;

        return new PagedResult<Artwork>
        {
            Items = list,
            TotalCount = total,
            Page = page,
            PageSize = PageSize
        };
    }

    public async Task<Artwork?> GetArtworkByIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task<int> CreateArtworkAsync(ArtworkDto dto)
    {
        var artwork = new Artwork
        {
            Title = dto.Title,
            Artist = dto.Artist,
            Medium = dto.Medium,
            DateCreated = dto.DateCreated,
            Department = dto.Department,
            AccessionNumber = dto.AccessionNumber,
            ImageUrl = dto.ImageUrl,
            IsOnDisplay = dto.IsOnDisplay
        };
        return await _repo.CreateAsync(artwork);
    }

    public async Task<bool> UpdateArtworkAsync(int id, ArtworkDto dto)
    {
        var artwork = new Artwork
        {
            ArtworkId = id,
            Title = dto.Title,
            Artist = dto.Artist,
            Medium = dto.Medium,
            DateCreated = dto.DateCreated,
            Department = dto.Department,
            AccessionNumber = dto.AccessionNumber,
            ImageUrl = dto.ImageUrl,
            IsOnDisplay = dto.IsOnDisplay
        };
        return await _repo.UpdateAsync(artwork);
    }

    public async Task<bool> DeleteArtworkAsync(int id) => await _repo.DeleteAsync(id);

    public async Task<IEnumerable<ConservationLog>> GetConservationLogsAsync(int artworkId)
        => await _repo.GetConservationLogsAsync(artworkId);

    public async Task<int> CreateConservationLogAsync(ConservationLogDto dto)
    {
        var log = new ConservationLog
        {
            ArtworkId = dto.ArtworkId,
            TreatmentType = dto.TreatmentType,
            Conservator = dto.Conservator,
            TreatmentDate = dto.TreatmentDate,
            Notes = dto.Notes,
            Status = dto.Status
        };
        return await _repo.CreateConservationLogAsync(log);
    }

    public async Task<IEnumerable<RightsLicensing>> GetRightsAsync(int artworkId)
        => await _repo.GetRightsAsync(artworkId);

    public async Task<int> CreateRightsAsync(RightsLicensingDto dto)
    {
        var rights = new RightsLicensing
        {
            ArtworkId = dto.ArtworkId,
            Licensee = dto.Licensee,
            UsageType = dto.UsageType,
            ExpiryDate = dto.ExpiryDate,
            Status = dto.Status
        };
        return await _repo.CreateRightsAsync(rights);
    }
}