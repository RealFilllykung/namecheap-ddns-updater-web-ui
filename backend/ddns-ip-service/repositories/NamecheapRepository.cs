using ddns_ip_service.infrastructures.interfaces.repositories;

namespace ddns_ip_service.repositories;

public class NamecheapRepository : INamecheapRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<NamecheapRepository> _logger;

    public NamecheapRepository(HttpClient httpClient, ILogger<NamecheapRepository> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<String> UpdateDdns(string query)
    {
        HttpResponseMessage response;
        try
        {
            _logger.LogInformation("Calling Namecheap DDNS URL for updating DDNS record.");
            response = await _httpClient.GetAsync($"/{query}");
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to update DDNS record with an exception message of: {e}");
            throw;
        }
        string responseString = await response.Content.ReadAsStringAsync();
        string errorCountSplit = responseString.Split("ErrCount")[1];
        int errorCount = Int32.Parse(errorCountSplit.Substring(1, errorCountSplit.Length - 3));
        if (errorCount > 0) throw new Exception("The query to Namecheap DDNS have an error.");
        return responseString;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}