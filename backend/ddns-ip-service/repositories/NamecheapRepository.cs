using ddns_ip_service.infrastructures.interfaces.repositories;

namespace ddns_ip_service.repositories;

public class NamecheapRepository : INamecheapRepository
{
    private readonly HttpClient _httpClient;

    public NamecheapRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<String> UpdateDdns(string query)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/{query}");
        string responseString = await response.Content.ReadAsStringAsync();
        return responseString;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}