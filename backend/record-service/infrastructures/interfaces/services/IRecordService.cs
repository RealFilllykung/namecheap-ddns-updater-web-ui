using record_service.models.requests;
using record_service.models.responses;

namespace record_service.infrastructures.interfaces.services;

public interface IRecordService
{
    public Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request);
}