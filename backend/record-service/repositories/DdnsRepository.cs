using record_service.infrastructures.interfaces.repositories;

namespace record_service.repositories;

public class DdnsRepository : IDdnsRepository
{
    private readonly ILogger<DdnsRepository> _logger;
    private readonly HttpClient _httpClient;

    public DdnsRepository(ILogger<DdnsRepository> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public async Task<HttpResponseMessage> UpdateDdnsRecord(string domain)
    {
        using (_httpClient)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{domain}");
            return response;
        }
    }
}