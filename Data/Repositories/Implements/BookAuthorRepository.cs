using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;

namespace Library.Data.Repositories.Implements;

public class BookAuthorRepository(ApplicationDbContext appDbContext) : Repository<BookAuthor>(appDbContext), IBookAuthorRepository;