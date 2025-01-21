namespace record_service.models.responses;

public class CreateRecordResponse
{
    public int id { get; set; }
    public string domain { get; set; }
    public string encryptedPassword { get; set; }
    public string ip { get; set; }
}