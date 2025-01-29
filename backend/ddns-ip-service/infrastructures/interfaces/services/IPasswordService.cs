namespace ddns_ip_service.infrastructures.interfaces.services;

public interface IPasswordService : IDisposable
{
    public Task<string> DecryptPassword(string encryptedPassword);
}