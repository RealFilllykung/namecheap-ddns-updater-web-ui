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

    public DdnsService(ILogger<DdnsService> logger, DatabaseContext databaseContext, IPasswordRepository passwordRepository, IIpRepository ipRepository)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _passwordRepository = passwordRepository;
        _ipRepository = ipRepository;
    }

    public async Task UpdateDdns(RecordModel request)
    {
        DecryptPasswordResponse decryptedPassword = await _passwordRepository.DecryptPassword(request.encryptedPassword);
        string domain = request.domain;
        string ip = await _ipRepository.GetCurrentPublicIp();
        string password = decryptedPassword.password;

        RecordModel? recordModel = await _databaseContext.Records.FindAsync(domain);
        if (ip != recordModel?.ip)
        {
            // Update the record
        }
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}