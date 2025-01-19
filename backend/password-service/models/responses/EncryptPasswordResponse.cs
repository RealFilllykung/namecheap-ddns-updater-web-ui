namespace password_service.models.responses;

public class EncryptPasswordResponse
{
    public string encryptedPassword { get; set; }
    public string iv { get; set; }
}