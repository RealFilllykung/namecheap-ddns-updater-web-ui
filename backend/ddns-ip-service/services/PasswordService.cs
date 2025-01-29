using ddns_ip_service.infrastructures.interfaces.repositories;
using ddns_ip_service.infrastructures.interfaces.services;
using ddns_ip_service.models.responses;

namespace ddns_ip_service.services;

public class PasswordService : IPasswordService
{
    private readonly IPasswordRepository _passwordRepository;
    private readonly ILogger<PasswordService> _logger;

    public PasswordService(IPasswordRepository passwordRepository, ILogger<PasswordService> logger)
    {
        _passwordRepository = passwordRepository;
        _logger = logger;
    }

    public void Dispose()
    {
        _passwordRepository.Dispose();
    }

    public async Task<string> DecryptPassword(string encryptedPassword)
    {
        DecryptPasswordResponse response = await _passwordRepository.DecryptPassword(encryptedPassword);
        return response.password;
    }
}