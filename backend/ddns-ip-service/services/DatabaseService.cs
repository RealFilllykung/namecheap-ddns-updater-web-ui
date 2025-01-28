using ddns_ip_service.infrastructures.databases;
using ddns_ip_service.infrastructures.interfaces.services;
using ddns_ip_service.models;
using Microsoft.EntityFrameworkCore;

namespace ddns_ip_service.services;

public class DatabaseService : IDatabaseService
{
    private readonly ILogger<DatabaseService> _logger;
    private readonly DatabaseContext _databaseContext;

    public DatabaseService(ILogger<DatabaseService> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    public void Dispose()
    {
        _databaseContext.Dispose();
    }

    public async Task<List<RecordModel?>> GetAllRecords()
    {
        return await _databaseContext.Records.ToListAsync();
    }

    public async Task<RecordModel?> GetRecord(string domain)
    {
        return await _databaseContext.Records.FindAsync(domain);
    }

    public async Task UpdateRecord(RecordModel record)
    {
        _databaseContext.Records.Update(record);
        await _databaseContext.SaveChangesAsync();
    }
}