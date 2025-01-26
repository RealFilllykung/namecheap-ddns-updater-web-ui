using record_service.infrastructures.interfaces.repositories;
using record_service.models.requests;

namespace record_service.repositories;

public class DdnsRepository : IDdnsRepository
{
    private readonly HttpClient _httpClient;

    public DdnsRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public async Task<HttpResponseMessage> UpdateDdnsRecord(UpdateDdnsRequest request)
    {
        using (_httpClient)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{request.domain}",request);
            return response;
        }
    }
}