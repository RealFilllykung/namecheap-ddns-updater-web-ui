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
            string responseBody = await _ipRepository.GetCurrentPublicIp();
            string ip = GetIpFromBody(responseBody);
            return ip;
        }
    }

    private string GetIpFromBody(string responseBody)
    {
        string[] colonSplit = responseBody.Split(':');
        string behindSubstring = colonSplit[1].Substring(1);
        string[] tagSplit = behindSubstring.Split('<');
        string ip = tagSplit[0];
        return ip;
    }

    public void Dispose()
    {
        _ipRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}