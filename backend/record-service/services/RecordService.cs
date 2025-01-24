using Microsoft.EntityFrameworkCore;
using record_service.infrastructures.databases;
using record_service.infrastructures.interfaces;
using record_service.infrastructures.interfaces.repositories;
using record_service.infrastructures.interfaces.services;
using record_service.models;
using record_service.models.requests;
using record_service.models.responses;

namespace record_service.services;

public class RecordService : IRecordService
{
    private readonly IIPService _ipService;
    private readonly ILogger<RecordService> _logger;
    private readonly IPasswordRepository _passwordRepository;
    private readonly DatabaseContext _databaseContext;

    public RecordService(IIPService ipService, ILogger<RecordService> logger, IPasswordRepository passwordRepository, DatabaseContext databaseContext)
    {
        _ipService = ipService;
        _logger = logger;
        _passwordRepository = passwordRepository;
        _databaseContext = databaseContext;
    }

    public async Task CreateRecord(CreateRecordRequest request)
    {
        _logger.LogInformation($"Creating new record for {request.domain}");
        _logger.LogInformation($"Getting current IP for {request.domain}");
        
        string ip = await _ipService.GetCurrentPublicIP();
        _logger.LogInformation($"Obtained public IP of {ip}");

        _logger.LogInformation($"Requesting password encryption");
        EncryptPasswordResponse? encryptPasswordResponse = await _passwordRepository.EncryptPassword(request.password);
        _logger.LogInformation($"Done encrypting password: {encryptPasswordResponse?.encryptedPassword}");
        
        _logger.LogInformation($"Recording the record into database.");

        RecordModel? recordModel = new RecordModel
        {
            encryptedPassword = encryptPasswordResponse?.encryptedPassword,
            domain = request.domain,
            ip = ip
        };
        
        _databaseContext.Records.Add(recordModel);
        await _databaseContext.SaveChangesAsync();
        
        _logger.LogInformation($"Successfully created new record for {request.domain}");
    }

    public async Task<RecordModel?> GetRecordByDomainName(string domainName)
    {
        RecordModel? response = await _databaseContext.Records.FindAsync(domainName);
        return response;
    }

    public async Task<List<RecordModel>> GetRecords()
    {
        List<RecordModel> response = await _databaseContext.Records.ToListAsync();
        return response;
    }

    public async Task UpdateRecord(UpdateRecordRequest record)
    {
        RecordModel recordModel = _databaseContext.Records.Find(record.domain);
        recordModel.encryptedPassword = record.password;
        recordModel.ip = await _ipService.GetCurrentPublicIP();
        _databaseContext.Records.Update(recordModel);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateRecord(RecordModel record)
    {
        _databaseContext.Records.Update(record);
        await _databaseContext.SaveChangesAsync();
    }
}