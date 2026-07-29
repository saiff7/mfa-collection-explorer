using Dapper;
using Microsoft.Data.SqlClient;
using MfaCollectionExplorer.Models;

namespace MfaCollectionExplorer.Repositories;

public class ArtworkRepository : IArtworkRepository
{
    private readonly string _connectionString;

    public ArtworkRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Artwork>> GetByDepartmentAsync(string department, int page, int pageSize)
    {
        using var conn = CreateConnection();
        return await conn.QueryAsync<Artwork>(
            "sp_GetArtworksByDepartment",
            new { Department = department, PageNumber = page, PageSize = pageSize },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<Artwork?> GetByIdAsync(int id)
    {
        using var conn = CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Artwork>(
            "SELECT * FROM Artworks WHERE ArtworkId = @Id", new { Id = id });
    }

    public async Task<int> CreateAsync(Artwork artwork)
    {
        using var conn = CreateConnection();
        var sql = @"INSERT INTO Artworks (Title, Artist, Medium, DateCreated, Department, AccessionNumber, ImageUrl, IsOnDisplay)
                    VALUES (@Title, @Artist, @Medium, @DateCreated, @Department, @AccessionNumber, @ImageUrl, @IsOnDisplay);
                    SELECT SCOPE_IDENTITY();";
        return await conn.ExecuteScalarAsync<int>(sql, artwork);
    }

    public async Task<bool> UpdateAsync(Artwork artwork)
    {
        using var conn = CreateConnection();
        var sql = @"UPDATE Artworks SET Title=@Title, Artist=@Artist, Medium=@Medium,
                    DateCreated=@DateCreated, Department=@Department, AccessionNumber=@AccessionNumber,
                    ImageUrl=@ImageUrl, IsOnDisplay=@IsOnDisplay
                    WHERE ArtworkId=@ArtworkId";
        var rows = await conn.ExecuteAsync(sql, artwork);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var conn = CreateConnection();
        var rows = await conn.ExecuteAsync("DELETE FROM Artworks WHERE ArtworkId=@Id", new { Id = id });
        return rows > 0;
    }

    public async Task<IEnumerable<ConservationLog>> GetConservationLogsAsync(int artworkId)
    {
        using var conn = CreateConnection();
        return await conn.QueryAsync<ConservationLog>(
            "SELECT * FROM ConservationLogs WHERE ArtworkId=@ArtworkId", new { ArtworkId = artworkId });
    }

    public async Task<int> CreateConservationLogAsync(ConservationLog log)
    {
        using var conn = CreateConnection();
        var sql = @"INSERT INTO ConservationLogs (ArtworkId, TreatmentType, Conservator, TreatmentDate, Notes, Status)
                    VALUES (@ArtworkId, @TreatmentType, @Conservator, @TreatmentDate, @Notes, @Status);
                    SELECT SCOPE_IDENTITY();";
        return await conn.ExecuteScalarAsync<int>(sql, log);
    }

    public async Task<IEnumerable<RightsLicensing>> GetRightsAsync(int artworkId)
    {
        using var conn = CreateConnection();
        return await conn.QueryAsync<RightsLicensing>(
            "SELECT * FROM RightsLicensing WHERE ArtworkId=@ArtworkId", new { ArtworkId = artworkId });
    }

    public async Task<int> CreateRightsAsync(RightsLicensing rights)
    {
        using var conn = CreateConnection();
        var sql = @"INSERT INTO RightsLicensing (ArtworkId, Licensee, UsageType, ExpiryDate, Status)
                    VALUES (@ArtworkId, @Licensee, @UsageType, @ExpiryDate, @Status);
                    SELECT SCOPE_IDENTITY();";
        return await conn.ExecuteScalarAsync<int>(sql, rights);
    }
}