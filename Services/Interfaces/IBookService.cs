using Library.Dto.Implements;
using Library.Entities.Implements;

namespace Library.Services.Interfaces;

/**
 * Interface for Book service
 * If this service needs to be extended, add methods here
 */
public interface IBookService : IService<Book>
{
    List<Book>? GetAllByAuthor(long authorId);
    List<Book>? GetAllByCategory(long categoryId);
    List<Book>? GetAllByTitle(string title);
    Book? Add(BookDto bookDto);
    Book? Update(long id, BookDto bookDto);
    List<AuthorDto>? GetAuthors(long bookId);
    List<CategoryDto>? GetCategories(long bookId);
}