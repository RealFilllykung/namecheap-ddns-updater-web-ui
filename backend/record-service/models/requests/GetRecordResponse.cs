namespace record_service.models.requests;

public class GetRecordResponse
{
    public string domain { get; set; }
    public string password { get; set; }
    public string ip { get; set; }
}