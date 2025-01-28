using ddns_ip_service.infrastructures.databases;
using ddns_ip_service.infrastructures.interfaces.repositories;
using ddns_ip_service.infrastructures.interfaces.services;
using ddns_ip_service.models;

namespace ddns_ip_service.services;

public class DdnsService : IDdnsService
{
    
    private readonly ILogger<DdnsService> _logger;
    private readonly DatabaseContext _databaseContext;
    private readonly IIpService _ipService;
    private readonly IDatabaseService _databaseService;
    private readonly INamecheapService _namecheapService;
    private readonly IPasswordService _passwordService;

    public DdnsService(ILogger<DdnsService> logger, DatabaseContext databaseContext, IIpService ipService, IDatabaseService databaseService, INamecheapService namecheapService, IPasswordService passwordService)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _ipService = ipService;
        _databaseService = databaseService;
        _namecheapService = namecheapService;
        _passwordService = passwordService;
    }

    public async Task UpdateDdns(string domain)
    {
        string ip = await _ipService.GetCurrentPublicIp();

        RecordModel? recordModel = await _databaseService.GetRecord(domain);

        if (recordModel != null) await UpdateDdnsRecord(recordModel, ip);
    }

    public async Task UpdateAllDdns()
    {
        string ip = await _ipService.GetCurrentPublicIp();
        List<RecordModel?> recordModels = await _databaseService.GetAllRecords();

        foreach (RecordModel? record in recordModels)
        {
            if (record != null) await UpdateDdnsRecord(record, ip);
        }
    }

    private async Task UpdateDdnsRecord(RecordModel record, string ip)
    {
        string password = await _passwordService.DecryptPassword(record.encryptedPassword);
        string query = _namecheapService.BuildNamecheapDdnsUpdateQuery(record?.domain, ip, password);
        await _namecheapService.UpdateDdns(query);
        record.ip = ip;
        await _databaseService.UpdateRecord(record);
    }

    public void Dispose()
    {
        _databaseContext.Dispose();
        _ipService.Dispose();
    }
}