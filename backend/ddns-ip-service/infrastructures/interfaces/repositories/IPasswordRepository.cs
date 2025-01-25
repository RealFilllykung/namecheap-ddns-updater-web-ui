using ddns_ip_service.models.responses;

namespace ddns_ip_service.infrastructures.interfaces.repositories;

public interface IPasswordRepository : IDisposable
{
    Task<EncryptPasswordResponse?> EncryptPassword(string password);
    Task<DecryptPasswordResponse> DecryptPassword(string encryptedPassword);
}