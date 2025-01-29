namespace password_service.models;

public class PasswordRecord
{
    public int id { get; set; }
    public string encryptedPassword { get; set; }
    public string iv { get; set; }
}