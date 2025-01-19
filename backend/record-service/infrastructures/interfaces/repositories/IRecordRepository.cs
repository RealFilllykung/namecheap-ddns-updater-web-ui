namespace record_service.infrastructures.interfaces.repositories;

public interface IRecordRepository
{
    Task<T> CreateRecord<T>(T record) where T : class;
    Task<T> ReadRecord<T>(T record) where T : class;
    Task<T> UpdateRecord<T>(T record) where T : class;
    Task<T> DeleteRecord<T>(T record) where T : class;
}