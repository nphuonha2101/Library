namespace Library.Services.Interfaces;

/**
 * Generic interface for services
 * Provide basic CRUD operations
 * Other services should implement this interface
 * @param <T> Entity type
 */
public interface IService<T>
{
    List<T> GetAll();
    T GetById(int id);
    T Add(T entity);
    bool Update(int id, T entity);
    bool Delete(int id);
}