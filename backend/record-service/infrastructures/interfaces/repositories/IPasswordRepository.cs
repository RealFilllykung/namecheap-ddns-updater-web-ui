using record_service.models.requests;
using record_service.models.responses;

namespace record_service.infrastructures.interfaces.repositories;

public interface IPasswordRepository
{
    Task<EncryptPasswordResponse?> EncryptPassword(string password);
    Task<DecryptPasswordResponse> DecryptPassword(string encryptedPassword);
}