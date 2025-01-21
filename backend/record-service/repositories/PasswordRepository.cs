using System.Text;
using Newtonsoft.Json;
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

    public async Task<EncryptPasswordResponse?> EncryptPassword(string password)
    {
        EncryptPasswordRequest request = new EncryptPasswordRequest
        {
            password = password
        };
        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await _httpClient.PostAsync("/password/EncryptPassword", stringContent);
        string responseString = await responseMessage.Content.ReadAsStringAsync();
        EncryptPasswordResponse? encryptPasswordResponse = JsonConvert.DeserializeObject<EncryptPasswordResponse>(responseString);
        return encryptPasswordResponse;
    }

    public Task<DecryptPasswordResponse> DecryptPassword(string encryptedPassword)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}