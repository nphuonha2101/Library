using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;

namespace Library.Data.Repositories.Implements;

public class CategoryRepository(ApplicationDbContext appDbContext)
    : Repository<Category>(appDbContext), ICategoryRepository;