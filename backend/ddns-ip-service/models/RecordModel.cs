using System.ComponentModel.DataAnnotations;

namespace ddns_ip_service.models;

public class RecordModel
{
    [Key]
    public string domain { get; set; }
    public string encryptedPassword { get; set; }
    public string ip { get; set; }
}