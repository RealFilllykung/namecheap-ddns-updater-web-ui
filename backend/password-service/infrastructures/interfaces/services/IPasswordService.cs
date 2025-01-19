using System.Threading.Tasks;

namespace password_service.infrastructures.interfaces.services;

public interface IPasswordService
{
    Task<string> GetAllPasswords();
    Task<string> CreateNewPassword(string password);
    Task<string> UpdatePassword(string oldPassword, string newPassword);
    Task<string> DeletePassword(string password);
}