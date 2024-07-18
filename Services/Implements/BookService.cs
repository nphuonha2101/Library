using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

/**
 * Book service
 * Implement IBookRepository
 */
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public List<Book> GetAllByAuthor(int authorId)
    {
        return _bookRepository.GetBooksByAuthorAsync(authorId).Result;
    }
    
    public List<Book> GetAll()
    {
        return  _bookRepository.GetAllAsync().Result;
    }

    public Book GetById(int id)
    {
        return _bookRepository.GetByIdAsync(id).Result;
    }

    public Book Add(Book entity)
    {
        return _bookRepository.AddAsync(entity).Result;
    }

    public bool Update(int id, Book entity)
    {
        return _bookRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(int id)
    {
        return _bookRepository.DeleteAsync(id).Result;
    }
}