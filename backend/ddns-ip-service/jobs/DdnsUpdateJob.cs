using ddns_ip_service.infrastructures.interfaces.services;
using Quartz;

namespace ddns_ip_service.jobs;

public class DdnsUpdateJob : IJob
{
    private readonly ILogger<DdnsUpdateJob> _logger;
    private readonly IDdnsService _ddnsService;

    public DdnsUpdateJob(ILogger<DdnsUpdateJob> logger, IDdnsService ddnsService)
    {
        _logger = logger;
        _ddnsService = ddnsService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"Updating all record.");
        await _ddnsService.UpdateAllDdns();
        _logger.LogInformation($"Done updating all record.");
    }
}