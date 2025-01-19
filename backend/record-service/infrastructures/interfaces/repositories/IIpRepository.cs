namespace record_service.infrastructures.interfaces.repositories;

public interface IIpRepository
{
    public Task<string> GetCurrentPublicIp();
}