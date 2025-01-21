using record_service.infrastructures.interfaces.repositories;
using record_service.models.requests;
using record_service.models.responses;

namespace record_service.repositories;

public class PasswordRepository : IPasswordRepository
{
    
    private readonly HttpClient _httpClient;

    public PasswordRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<EncryptPasswordResponse> EncryptPassword(EncryptPasswordRequest request)
    {
        
        throw new NotImplementedException();
    }

    public Task<DecryptPasswordResponse> DecryptPassword(DecryptPasswordRequest request)
    {
        throw new NotImplementedException();
    }
}