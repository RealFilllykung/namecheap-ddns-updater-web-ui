using System.Threading.Tasks;
using password_service.models;
using password_service.models.responses;

namespace password_service.infrastructures.interfaces.services;

public interface IPasswordService
{
    Task<EncryptPasswordResponse> EncryptPassword(EncryptPasswordRequest request);
    Task<DecryptPasswordResponse> DecryptPassword(DecryptPasswordRequest request);
}