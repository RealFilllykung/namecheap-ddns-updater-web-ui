namespace record_service.infrastructures.interfaces.repositories;

public interface IDdnsRepository : IDisposable
{
    public Task<HttpResponseMessage> UpdateDdnsRecord(string domain);
}