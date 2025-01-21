using record_service.infrastructures.interfaces;
using record_service.infrastructures.interfaces.services;
using record_service.models.requests;
using record_service.models.responses;

namespace record_service.services;

public class RecordService : IRecordService
{
    private readonly IIPService _ipService;
    private readonly ILogger<RecordService> _logger;

    public RecordService(IIPService ipService, ILogger<RecordService> logger)
    {
        _ipService = ipService;
        _logger = logger;
    }

    public async Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request)
    {
        _logger.LogInformation($"Creating new record for {request.domain}");
        _logger.LogInformation($"Getting current IP for {request.domain}");
        
        string ip = await _ipService.GetCurrentPublicIP();
        _logger.LogInformation($"Obtained public IP of {ip}");

        CreateRecordResponse response = new CreateRecordResponse
        {
            ip = ip,
            domain = request.domain
        };

        return response;
    }
}