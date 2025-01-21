using record_service.infrastructures.interfaces;
using record_service.infrastructures.interfaces.repositories;
using record_service.infrastructures.interfaces.services;
using record_service.models.requests;
using record_service.models.responses;

namespace record_service.services;

public class RecordService : IRecordService
{
    private readonly IIPService _ipService;
    private readonly ILogger<RecordService> _logger;
    private readonly IPasswordRepository _passwordRepository;

    public RecordService(IIPService ipService, ILogger<RecordService> logger, IPasswordRepository passwordRepository)
    {
        _ipService = ipService;
        _logger = logger;
        _passwordRepository = passwordRepository;
    }

    public async Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request)
    {
        _logger.LogInformation($"Creating new record for {request.domain}");
        _logger.LogInformation($"Getting current IP for {request.domain}");
        
        string ip = await _ipService.GetCurrentPublicIP();
        _logger.LogInformation($"Obtained public IP of {ip}");

        EncryptPasswordResponse? encryptPasswordResponse = await _passwordRepository.EncryptPassword(request.password);
        CreateRecordResponse response = new CreateRecordResponse
        {
            ip = ip,
            domain = request.domain,
            encryptedPassword = encryptPasswordResponse?.encryptedPassword,
        };

        return response;
    }
}