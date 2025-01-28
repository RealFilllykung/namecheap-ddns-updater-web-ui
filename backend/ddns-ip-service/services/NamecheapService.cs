using ddns_ip_service.infrastructures.interfaces.repositories;
using ddns_ip_service.infrastructures.interfaces.services;

namespace ddns_ip_service.services;

public class NamecheapService : INamecheapService
{
    private readonly ILogger<NamecheapService> _logger;
    private readonly INamecheapRepository _namecheapRepository;

    public NamecheapService(ILogger<NamecheapService> logger, INamecheapRepository namecheapRepository)
    {
        _logger = logger;
        _namecheapRepository = namecheapRepository;
    }

    public void Dispose()
    {
        _namecheapRepository.Dispose();
    }

    public string BuildNamecheapDdnsUpdateQuery(string domain, string ip, string password)
    {
        string host = domain.Split('.')[0];
        string domainName = domain.Split('.')[1] + "." + domain.Split('.')[2];
        return $"update?host={host}&domain={domainName}&password={password}&ip={ip}";
    }

    public async Task<string> UpdateDdns(string query)
    {
        return await _namecheapRepository.UpdateDdns(query);
    }
}