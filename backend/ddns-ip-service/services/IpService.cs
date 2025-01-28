using ddns_ip_service.infrastructures.interfaces.repositories;
using ddns_ip_service.infrastructures.interfaces.services;

namespace ddns_ip_service.services;

public class IpService : IIpService
{
    
    private readonly IIpRepository _ipRepository;
    private readonly ILogger<IpService> _logger;

    public IpService(IIpRepository ipRepository, ILogger<IpService> logger)
    {
        _ipRepository = ipRepository;
        _logger = logger;
    }

    public Task<string> GetCurrentPublicIp()
    {
        return _ipRepository.GetCurrentPublicIp();
    }

    public void Dispose()
    {
        _ipRepository.Dispose();
    }
}