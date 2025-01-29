namespace ddns_ip_service.infrastructures.interfaces.services;

public interface IIpService : IDisposable
{
    Task<string> GetCurrentPublicIp();
}