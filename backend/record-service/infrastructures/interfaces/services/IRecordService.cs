using record_service.models;
using record_service.models.requests;
using record_service.models.responses;

namespace record_service.infrastructures.interfaces.services;

public interface IRecordService
{
    public Task CreateRecord(CreateRecordRequest request);
    public Task<RecordModel?> GetRecordByDomainName(string domainName);
    public Task<List<RecordModel>> GetRecords();
    public Task UpdateRecord(UpdateRecordRequest record);
}