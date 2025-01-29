namespace record_service.infrastructures.interfaces.services;

public interface IDdnsService : IDisposable
{
    public Task UpdateDdnsRecord(string domain);
}