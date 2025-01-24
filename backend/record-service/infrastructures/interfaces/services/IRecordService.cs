using record_service.models;
using record_service.models.requests;
using record_service.models.responses;

namespace record_service.infrastructures.interfaces.services;

public interface IRecordService : IDisposable
{
    public Task CreateRecord(CreateRecordRequest request);
    public Task<GetRecordResponse?> GetRecordByDomainName(string domainName);
    public Task<List<GetRecordResponse>> GetRecords();
    public Task UpdateRecord(UpdateRecordRequest record);
    public Task DeleteRecord(string domainName);
}