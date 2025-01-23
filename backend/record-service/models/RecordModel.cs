using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace record_service.models;

public class RecordModel
{
    [Key]
    public string domain { get; set; }
    public string encryptedPassword { get; set; }
    public string ip { get; set; }
}