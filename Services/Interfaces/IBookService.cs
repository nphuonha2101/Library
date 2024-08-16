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
    List<Book> GetAllByCategory(int categoryId);
    Book Add(BookDto bookDto);
    List<AuthorDto> GetAuthors(long bookId);
    List<CategoryDto> GetCategories(long bookId);
}