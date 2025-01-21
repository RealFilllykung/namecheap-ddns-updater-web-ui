using record_service.infrastructures.interfaces.repositories;

namespace record_service.repositories;

public class IpRepository : IIpRepository
{
    
    private readonly HttpClient _httpClient;

    public IpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetCurrentPublicIp()
    {
        HttpResponseMessage responseMessage = await _httpClient.GetAsync("");
        return await responseMessage.Content.ReadAsStringAsync();
    }
}