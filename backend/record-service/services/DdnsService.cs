using record_service.infrastructures.interfaces.repositories;
using record_service.infrastructures.interfaces.services;

namespace record_service.services;

public class DdnsService : IDdnsService
{
    private readonly ILogger<DdnsService> _logger;
    private readonly IDdnsRepository _ddnsRepository;

    public DdnsService(ILogger<DdnsService> logger, IDdnsRepository ddnsRepository)
    {
        _logger = logger;
        _ddnsRepository = ddnsRepository;
    }

    public void Dispose()
    {
        _ddnsRepository.Dispose();
    }

    public async Task UpdateDdnsRecord(string domain)
    {
        using (_ddnsRepository)
        {
            _logger.LogInformation($"Updating DDNS record for domain {domain}");
            HttpResponseMessage responseMessage = await _ddnsRepository.UpdateDdnsRecord(domain);
            _logger.LogInformation($"Received DDNS record update with the response code of {responseMessage.StatusCode}");
            if (!responseMessage.IsSuccessStatusCode) throw new Exception(responseMessage.ReasonPhrase);
        }
    }
}