using Library.Data.Repositories.Interfaces;
using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

/**
 * Book service
 * Implement IBookRepository
 */
public class BookService(IBookRepository bookRepository) : IBookService
{
    public List<Book>? GetAllByAuthor(long authorId)
    {
        return bookRepository.GetBooksByAuthorAsync(authorId).Result;
    }

    public List<Book>? GetAllByCategory(long categoryId)
    {
        return bookRepository.GetBooksByCategoryAsync(categoryId).Result;
    }

    public List<Book>? GetAllByTitle(string title)
    {
        return bookRepository.GetBooksByTitleAsync(title).Result;
    }

    public List<Book>? GetAll()
    {
        return bookRepository.GetAllAsync().Result;
    }

    public Book? GetById(long id)
    {
        return bookRepository.GetByIdAsync(id).Result;
    }

    public Book? Add(Book entity)
    {
        return bookRepository.AddAsync(entity).Result;
    }

    public Book? Add(BookDto dto)
    {
        return bookRepository.AddAsync(dto).Result;
    }

    public Book? Update(long id, BookDto bookDto)
    {
        return bookRepository.UpdateAsync(id, bookDto).Result;
    }

    public List<Author>? GetAuthors(long bookId)
    {
        return bookRepository.GetAuthorsAsync(bookId).Result;
        
    }

    public List<Category>? GetCategories(long bookId)
    {
        return bookRepository.GetCategoriesAsync(bookId).Result;
    }

    public Book? Update(long id, Book entity)
    {
        return bookRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(long id)
    {
        return bookRepository.DeleteAsync(id).Result;
    }
}