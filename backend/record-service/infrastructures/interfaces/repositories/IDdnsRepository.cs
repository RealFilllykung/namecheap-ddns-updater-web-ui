using record_service.models.requests;

namespace record_service.infrastructures.interfaces.repositories;

public interface IDdnsRepository : IDisposable
{
    public Task<HttpResponseMessage> UpdateDdnsRecord(UpdateDdnsRequest domain);
}