using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<List<Author>> GetAllAsync()
    {
        return await AppDbContext.Authors.ToListAsync();
    }

    public async Task<Author> GetByIdAsync(long id)
    {
        return await AppDbContext.Authors.FindAsync(id);
    }
}