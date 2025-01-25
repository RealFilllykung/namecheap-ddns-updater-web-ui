namespace ddns_ip_service.infrastructures.interfaces.repositories;

public interface INamecheapRepository : IDisposable
{
    public Task<String> UpdateDdns(string query);
}