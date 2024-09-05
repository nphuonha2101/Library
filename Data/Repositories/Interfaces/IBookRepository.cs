using Library.Dto.Implements;
using Library.Entities.Implements;

namespace Library.Data.Repositories.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<List<Book>?> GetBooksByAuthorAsync(long authorId);
    Task<Book?> AddAsync(BookDto bookDto);
    Task<Book?> UpdateAsync(long id, BookDto bookDto);
    Task<List<Author>?> GetAuthorsAsync(long bookId);
    Task<List<Category>?> GetCategoriesAsync(long bookId);
    Task<List<Book>?> GetBooksByCategoryAsync(long categoryId);
    Task<List<Book>?> GetBooksByTitleAsync(string title);
}