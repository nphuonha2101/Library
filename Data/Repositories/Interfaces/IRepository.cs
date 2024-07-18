using Library.Entities;

namespace Library.Data.Repositories.Interfaces;

/**
 * Interface for Repository
 * Define basic CRUD methods
 */
public interface IRepository<T> where T : IEntity
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<bool> UpdateAsync(int id, T entity);
    Task<bool> DeleteAsync(int id);
}