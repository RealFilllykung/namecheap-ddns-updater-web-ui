namespace record_service.infrastructures.interfaces.services;

public interface IIPService : IDisposable
{
    public Task<string> GetCurrentPublicIP();
}