namespace Library.Services.Interfaces;

/**
 * Generic interface for services
 * Provide basic CRUD operations
 * Other services should implement this interface
 * @param
 * <T> Entity type
 */
public interface IService<T>
{
    List<T> GetAll();
    T GetById(long id);
    T Add(T entity);
    bool Update(long id, T entity);
    bool Delete(long id);
}