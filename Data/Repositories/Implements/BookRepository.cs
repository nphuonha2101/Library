using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class BookRepository: Repository<Book>, IBookRepository
{
    public BookRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    // Using LinQ to get books by author
    public async Task<List<Book>> GetBooksByAuthorAsync(int authorId)
    {
          return await AppDbContext.Books.Where(b => b.Id == authorId).ToListAsync() ;
    }
}