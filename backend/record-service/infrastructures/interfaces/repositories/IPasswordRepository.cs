namespace record_service.infrastructures.interfaces.repositories;

public interface IPasswordRepository
{
    Task<T> CreatePassword<T>(T record) where T : class;
    Task<T> ReadPassword<T>(T record) where T : class;
    Task<T> UpdatePassword<T>(T record) where T : class;
    Task<T> DeletePassword<T>(T record) where T : class;
}