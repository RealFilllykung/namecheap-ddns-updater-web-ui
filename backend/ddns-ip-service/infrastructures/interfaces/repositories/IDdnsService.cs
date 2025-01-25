using ddns_ip_service.models;
using ddns_ip_service.models.requests;

namespace ddns_ip_service.infrastructures.interfaces.repositories;

public interface IDdnsService : IDisposable
{
    Task UpdateDdns(string request);
}