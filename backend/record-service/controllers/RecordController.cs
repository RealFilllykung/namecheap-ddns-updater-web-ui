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
        await _recordService.CreateRecord(request);
    }
    
    [HttpGet("/{domainName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<RecordModel> GetRecordByDomain(string domainName)
    {
        _logger.LogInformation($"Getting record for {domainName}");
        RecordModel response = await _recordService.GetRecordByDomainName(domainName);
        return response;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<RecordModel>> GetRecords()
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string DeleteRecord()
    {
        return "Hello World!";
    }
}