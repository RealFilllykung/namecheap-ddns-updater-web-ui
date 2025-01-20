using System.ComponentModel.DataAnnotations;

namespace record_service.models;

public class RecordModel
{
    [Key]
    public int id { get; set; }
    public string domain { get; set; }
    public string encryptedPassword { get; set; }
    public string ip { get; set; }
}