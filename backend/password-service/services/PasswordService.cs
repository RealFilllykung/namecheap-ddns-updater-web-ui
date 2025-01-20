
using System.Security.Cryptography;
using System.Text;
using password_service.infrastructures.interfaces.services;
using password_service.models;
using password_service.models.responses;

namespace password_service.services;

public class PasswordService : IPasswordService
{
    private readonly ILogger<PasswordService> _logger;
    private RSA _rsa;

    public PasswordService(ILogger<PasswordService> logger)
    {
        _logger = logger;
        
        string aesKeyPath = Path.Combine("data", "asymmetricKey.pem");
        string pemFileContent = File.ReadAllText(aesKeyPath);
        _rsa = RSA.Create();
        _rsa.ImportFromPem(pemFileContent);
    }

    public async Task<EncryptPasswordResponse> EncryptPassword(EncryptPasswordRequest request)
    {
        EncryptPasswordResponse response = new EncryptPasswordResponse();
        byte[] passwordBytes = Encoding.ASCII.GetBytes(request.password);
        byte[] encryptedPasswordBytes;
        encryptedPasswordBytes = _rsa.Encrypt(passwordBytes, RSAEncryptionPadding.Pkcs1);
        string encryptedPasswordString = Convert.ToBase64String(encryptedPasswordBytes);
        response.encryptedPassword = encryptedPasswordString;
        return response;
    }

    public async Task<DecryptPasswordResponse> DecryptPassword(DecryptPasswordRequest request)
    {
        DecryptPasswordResponse response = new DecryptPasswordResponse();
        return response;
    }
    
}