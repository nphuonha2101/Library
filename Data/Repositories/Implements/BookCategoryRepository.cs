using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;

namespace Library.Data.Repositories.Implements;

public class BookCategoryRepository(ApplicationDbContext appDbContext) : Repository<BookCategory>(appDbContext), IBookCategoryRepository;