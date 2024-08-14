using Library.Dto.Implements;
using Library.Entities.Implements;

namespace Library.Services.Interfaces;

/**
 * Interface for Book service
 * If this service needs to be extended, add methods here
 */
public interface IBookService : IService<Book>
{
    List<Book> GetAllByAuthor(int authorId);
    Book Add(BookDto bookDto);
}