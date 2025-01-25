using record_service.infrastructures.interfaces.repositories;
using record_service.infrastructures.interfaces.services;

namespace record_service.services;

public class IpService : IIPService
{

    private readonly IIpRepository _ipRepository;

    public IpService(IIpRepository ipRepository)
    {
        _ipRepository = ipRepository;
    }

    public async Task<string> GetCurrentPublicIP()
    {
        using (_ipRepository)
        {
            return await _ipRepository.GetCurrentPublicIp();
        }
    }

    public void Dispose()
    {
        _ipRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}