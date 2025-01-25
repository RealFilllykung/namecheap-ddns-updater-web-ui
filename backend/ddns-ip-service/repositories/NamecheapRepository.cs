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
        string errorSplit = responseString.Split("Err")[0];
        if (errorSplit.Length > 0) throw new Exception("There was an error updating to Namecheap");
        return responseString;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}