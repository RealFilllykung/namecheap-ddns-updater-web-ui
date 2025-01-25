using ddns_ip_service.infrastructures.interfaces.repositories;

namespace ddns_ip_service.repositories;

public class IpRepository : IIpRepository
{
    
    private readonly HttpClient _httpClient;

    public IpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetCurrentPublicIp()
    {
        using (_httpClient)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("");
            return await responseMessage.Content.ReadAsStringAsync();
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}