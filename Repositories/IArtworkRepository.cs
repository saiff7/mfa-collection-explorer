using MfaCollectionExplorer.Models;

namespace MfaCollectionExplorer.Repositories;

public interface IArtworkRepository
{
    Task<IEnumerable<Artwork>> GetByDepartmentAsync(string department, int page, int pageSize);
    Task<Artwork?> GetByIdAsync(int id);
    Task<int> CreateAsync(Artwork artwork);
    Task<bool> UpdateAsync(Artwork artwork);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ConservationLog>> GetConservationLogsAsync(int artworkId);
    Task<int> CreateConservationLogAsync(ConservationLog log);
    Task<IEnumerable<RightsLicensing>> GetRightsAsync(int artworkId);
    Task<int> CreateRightsAsync(RightsLicensing rights);
}