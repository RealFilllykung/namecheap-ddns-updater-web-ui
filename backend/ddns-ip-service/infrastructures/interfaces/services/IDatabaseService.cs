using ddns_ip_service.models;

namespace ddns_ip_service.infrastructures.interfaces.services;

public interface IDatabaseService : IDisposable
{
    Task<List<RecordModel?>> GetAllRecords();
    Task<RecordModel?> GetRecord(string domain);
    Task UpdateRecord(RecordModel record);
}