namespace ddns_ip_service.infrastructures.interfaces.repositories;

public interface IIpRepository: IDisposable
{
    public Task<string> GetCurrentPublicIp();
}