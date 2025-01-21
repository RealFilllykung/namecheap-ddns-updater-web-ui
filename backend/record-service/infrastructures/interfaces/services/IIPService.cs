namespace record_service.infrastructures.interfaces.services;

public interface IIPService
{
    public Task<string> GetCurrentPublicIP();
}