using System.Threading.Tasks;

namespace password_service.infrastructures.interfaces.repositories;

public interface IPasswordDatabaseRepository
{
    Task<string> GetPasswords();
    Task<string> CreatePassword(string password);
    Task<string> UpdatePassword(string password);
    Task<string> DeletePassword(string password);
}