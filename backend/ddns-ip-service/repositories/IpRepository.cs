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
            string stringContent = await responseMessage.Content.ReadAsStringAsync();
            return GetIpFromBody(stringContent);
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
        _httpClient.Dispose();
    }
}