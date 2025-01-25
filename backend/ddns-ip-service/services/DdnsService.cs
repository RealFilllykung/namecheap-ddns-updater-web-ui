using ddns_ip_service.infrastructures.databases;
using ddns_ip_service.infrastructures.interfaces;
using ddns_ip_service.infrastructures.interfaces.repositories;
using ddns_ip_service.models;
using ddns_ip_service.models.requests;
using ddns_ip_service.models.responses;

namespace ddns_ip_service.services;

public class DdnsService : IDdnsService
{
    
    private readonly ILogger<DdnsService> _logger;
    private readonly DatabaseContext _databaseContext;
    private readonly IPasswordRepository _passwordRepository;
    private readonly IIpRepository _ipRepository;
    private readonly INamecheapRepository _namecheapRepository;

    public DdnsService(ILogger<DdnsService> logger, DatabaseContext databaseContext, IPasswordRepository passwordRepository, IIpRepository ipRepository, INamecheapRepository namecheapRepository)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _passwordRepository = passwordRepository;
        _ipRepository = ipRepository;
        _namecheapRepository = namecheapRepository;
    }

    public async Task UpdateDdns(string domain)
    {
        string ip = await _ipRepository.GetCurrentPublicIp();

        RecordModel? recordModel = await _databaseContext.Records.FindAsync(domain);
        
        string password = _passwordRepository.DecryptPassword(recordModel?.encryptedPassword).Result.password;
        string query = BuildNamecheapDdnsUpdateQuery(domain,ip,password);
        _namecheapRepository.UpdateDdns(query);
        recordModel.ip = ip;
        _databaseContext.Records.Update(recordModel);
        await _databaseContext.SaveChangesAsync();
    }

    private string BuildNamecheapDdnsUpdateQuery(string domain, string ip, string password)
    {
        string host = domain.Split('.')[0];
        string domainName = domain.Split('.')[1] + "." + domain.Split('.')[2];
        return $"update?host={host}&domain={domainName}&password={password}&ip={ip}";
    }

    public void Dispose()
    {
        _databaseContext.Dispose();
        _passwordRepository.Dispose();
        _ipRepository.Dispose();
        _namecheapRepository.Dispose();
    }
}