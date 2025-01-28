namespace ddns_ip_service.infrastructures.interfaces.services;

public interface INamecheapService : IDisposable
{
    public string BuildNamecheapDdnsUpdateQuery(string domain, string ip,string password);
    public Task<string> UpdateDdns(string query);
}