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

    public async Task<List<Book>> GetBooksByAuthorAsync(int authorId)
    {
        var query = from book in AppDbContext.Books
            where book.BookAuthors.Any(bookAuthor => bookAuthor.AuthorId == authorId)
            select book;

        return await query.ToListAsync();
    }
}