using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;

namespace Library.Data.Repositories.Implements;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}