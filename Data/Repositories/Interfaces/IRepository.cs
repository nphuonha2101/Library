using Library.Entities;

namespace Library.Data.Repositories.Interfaces;

/**
 * Interface for Repository
 * Define basic CRUD methods
 */
public interface IRepository<T> where T : IEntity
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(long id);
    Task<T> AddAsync(T entity);
    Task<bool> UpdateAsync(long id, T entity);
    Task<bool> DeleteAsync(long id);
}