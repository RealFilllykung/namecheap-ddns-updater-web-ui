namespace ddns_ip_service.infrastructures.interfaces.services;

public interface IDdnsService : IDisposable
{
    Task UpdateDdns(string request);
    Task UpdateAllDdns();
}