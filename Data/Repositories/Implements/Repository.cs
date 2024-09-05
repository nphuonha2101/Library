using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

/**
 * Repository
 * Implement IRepository
 * This class is used to implement basic CRUD methods
 * Other repositories will inherit from this class
 */
public abstract class Repository<T>(ApplicationDbContext appDbContext) : IRepository<T>
    where T : class, IEntity
{
    protected readonly ApplicationDbContext AppDbContext = appDbContext;
    protected readonly DbSet<T> Entities = appDbContext.Set<T>();

    public virtual async Task<List<T>?> GetAllAsync()
    {
        return await Entities.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(long id)
    {
        var entity = await Entities.FindAsync(id);
        if (entity == null) throw new Exception("Entity not found with id: " + id);

        return entity;
    }

    public virtual async Task<T?> AddAsync(T entity)
    {
        await Entities.AddAsync(entity);
        await AppDbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T?> UpdateAsync(long id, T entity)
    {
        var existingEntity = await Entities.FindAsync(id);
        if (existingEntity == null)
            throw new Exception("Entity not found with id: " + id);

        AppDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        AppDbContext.Entry(existingEntity).Property(e => e.GetId()).IsModified = false;
        await AppDbContext.SaveChangesAsync();
        return existingEntity;
    }

    public virtual async Task<bool> DeleteAsync(long id)
    {
        var existingEntity = await Entities.FindAsync(id);
        if (existingEntity == null)
            return false;

        Entities.Remove(existingEntity);
        await AppDbContext.SaveChangesAsync();
        return true;
    }
}