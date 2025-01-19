
using System.Security.Cryptography;
using password_service.infrastructures.interfaces.services;
using password_service.models;
using password_service.models.responses;

namespace password_service.services;

public class PasswordService : IPasswordService
{
    private readonly ILogger<PasswordService> _logger;
    private Aes _aes;

    public PasswordService(ILogger<PasswordService> logger)
    {
        _logger = logger;
        _aes = Aes.Create();
        string aesKeyPath = Path.Combine("..", "data", "symmetricKey.txt");
    }

    public async Task<EncryptPasswordResponse> EncryptPassword(EncryptPasswordRequest request)
    {
        EncryptPasswordResponse response = new EncryptPasswordResponse();
        return response;
    }

    public async Task<DecryptPasswordResponse> DecryptPassword(DecryptPasswordRequest request)
    {
        DecryptPasswordResponse response = new DecryptPasswordResponse();
        return response;
    }
    
}