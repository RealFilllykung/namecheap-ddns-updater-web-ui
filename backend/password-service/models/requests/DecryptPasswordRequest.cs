namespace password_service.models;

public class DecryptPasswordRequest
{
    public string encryptedPassword { get; set; }
    public string iv { get; set; }
}