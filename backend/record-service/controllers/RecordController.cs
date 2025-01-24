using Microsoft.AspNetCore.Mvc;
using record_service.infrastructures.interfaces.services;
using record_service.models;
using record_service.models.requests;
using record_service.models.responses;

namespace record_service.controllers;

[ApiController]
[Route("record")]
public class RecordController : ControllerBase
{
    private readonly IRecordService _recordService;
    private readonly ILogger<RecordController> _logger;
    
    public RecordController(IRecordService recordService, ILogger<RecordController> logger)
    {
        _recordService = recordService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task CreateRecord([FromBody] CreateRecordRequest request)
    {
        _logger.LogInformation($"Creating record for {request.domain}");
        await _recordService.CreateRecord(request);
        _logger.LogInformation($"Done creating record for {request.domain}");
    }
    
    [HttpGet("{domainName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<GetRecordResponse> GetRecordByDomain(string domainName)
    {
        _logger.LogInformation($"Getting record for {domainName}");
        GetRecordResponse response = await _recordService.GetRecordByDomainName(domainName);
        _logger.LogInformation($"Done getting record for {domainName}");
        return response;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<GetRecordResponse>> GetRecords()
    {
        _logger.LogInformation("Getting records");
        return await _recordService.GetRecords();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task PutRecord([FromBody] UpdateRecordRequest record)
    {
        _logger.LogInformation($"Updating record {record.domain}");
        await _recordService.UpdateRecord(record);
        _logger.LogInformation($"Done update record {record.domain}");
    }

    [HttpDelete]
    [Route("{domainName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task DeleteRecord(string domainName)
    {
        _logger.LogInformation($"Deleting record {domainName}");
        await _recordService.DeleteRecord(domainName);
        _logger.LogInformation($"Done delete record {domainName}");
    }
}