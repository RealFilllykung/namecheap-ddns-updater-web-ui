using Microsoft.AspNetCore.Mvc;

namespace record_service.controllers;

[ApiController]
[Route("record")]
public class RecordController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string GetRecord()
    {
        return "Hello World!";
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string PostRecord()
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