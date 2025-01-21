using Microsoft.AspNetCore.Mvc;
using record_service.infrastructures.interfaces.services;
using record_service.models.requests;
using record_service.models.responses;

namespace record_service.controllers;

[ApiController]
[Route("record")]
public class RecordController : ControllerBase
{
    private readonly IRecordService _recordService;
    
    public RecordController(IIPService ipService, IRecordService recordService)
    {
        _recordService = recordService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<CreateRecordResponse> CreateRecord([FromBody] CreateRecordRequest request)
    {
        CreateRecordResponse response = await _recordService.CreateRecord(request);
        return response;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string GetRecord()
    {
        return "Hello World!";
    }



    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string PutRecord()
    {
        return "Hello World!";
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string DeleteRecord()
    {
        return "Hello World!";
    }
}