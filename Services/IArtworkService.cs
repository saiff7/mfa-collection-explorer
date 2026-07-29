using MfaCollectionExplorer.Models;
using MfaCollectionExplorer.Models.DTOs;

namespace MfaCollectionExplorer.Services;

public interface IArtworkService
{
    Task<PagedResult<Artwork>> GetArtworksAsync(string department, int page, string search);
    Task<Artwork?> GetArtworkByIdAsync(int id);
    Task<int> CreateArtworkAsync(ArtworkDto dto);
    Task<bool> UpdateArtworkAsync(int id, ArtworkDto dto);
    Task<bool> DeleteArtworkAsync(int id);
    Task<IEnumerable<ConservationLog>> GetConservationLogsAsync(int artworkId);
    Task<int> CreateConservationLogAsync(ConservationLogDto dto);
    Task<IEnumerable<RightsLicensing>> GetRightsAsync(int artworkId);
    Task<int> CreateRightsAsync(RightsLicensingDto dto);
}