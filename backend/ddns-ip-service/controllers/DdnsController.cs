using ddns_ip_service.infrastructures.interfaces.repositories;
using ddns_ip_service.models;
using ddns_ip_service.models.requests;
using Microsoft.AspNetCore.Mvc;

namespace ddns_ip_service.controllers;

[ApiController]
[Route("ddns")]
public class DdnsController : ControllerBase
{
    private readonly ILogger<DdnsController> _logger;
    private readonly IDdnsService _ddnsService;

    public DdnsController(ILogger<DdnsController> logger, IDdnsService ddnsService)
    {
        _logger = logger;
        _ddnsService = ddnsService;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task UpdateDdns([FromBody] UpdateDdnsRequest request)
    {
        _logger.LogInformation($"Updating DDNS for {request.domain}");
        await _ddnsService.UpdateDdns(request.domain);
        _logger.LogInformation($"Updated DDNS for {request.domain}");
    }
}