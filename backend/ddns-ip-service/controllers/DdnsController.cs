using ddns_ip_service.infrastructures.interfaces.repositories;
using ddns_ip_service.infrastructures.interfaces.services;
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
    [Route("/{domain}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task UpdateDdns(string domain)
    {
        _logger.LogInformation($"Updating DDNS for {domain}");
        await _ddnsService.UpdateDdns(domain);
        _logger.LogInformation($"Updated DDNS for {domain}");
    }

    [HttpPut]
    [Route("/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task UpdateAllDdns()
    {
        _logger.LogInformation($"Updating all domain record");
        await _ddnsService.UpdateAllDdns();
        _logger.LogInformation($"Updated all domain record");
    }
}