namespace ddns_ip_service.models.requests;

public class UpdateDdnsRequest
{
    public string domain { get; set; }
    public string password { get; set; }
    public string ip { get; set; }
}