using Library.Dto.Implements;
using Library.Entities.Implements;

namespace Library.Data.Repositories.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<List<Book>> GetBooksByAuthorAsync(int authorId);
    Task<Book> AddAsync(BookDto bookDto);
}