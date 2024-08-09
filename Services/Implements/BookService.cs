using Library.Data.Repositories.Interfaces;
using Library.Dto.Implements;
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
    private readonly IBookAuthorRepository _bookAuthorRepository;
    private readonly IBookCategoryRepository _bookCategoryRepository;

    public BookService(IBookRepository bookRepository, IBookAuthorRepository bookAuthorRepository,
        IBookCategoryRepository bookCategoryRepository)
    {
        _bookRepository = bookRepository;
        _bookAuthorRepository = bookAuthorRepository;
        _bookCategoryRepository = bookCategoryRepository;
    }

    public List<Book> GetAllByAuthor(int authorId)
    {
        return _bookRepository.GetBooksByAuthorAsync(authorId).Result;
    }

    public List<Book> GetAll()
    {
        return _bookRepository.GetAllAsync().Result;
    }

    public Book GetById(int id)
    {
        return _bookRepository.GetByIdAsync(id).Result;
    }

    public Book Add(Book entity)
    {
        return _bookRepository.AddAsync(entity).Result;
    }

    public Book Add(BookDto dto)
    {
        var (book, intermediateEntities) = dto.ToEntities();

        var bookEntity = (Book)book;

        var bookAdded = _bookRepository.AddAsync(bookEntity);

        foreach (var entity in intermediateEntities)
        {
            switch (entity)
            {
                case BookAuthor bookAuthor:
                    bookAuthor.BookId = bookEntity.Id;
                    _bookAuthorRepository.AddAsync(bookAuthor);
                    break;

                case BookCategory bookCategory:
                    bookCategory.BookId = bookEntity.Id;
                    _bookCategoryRepository.AddAsync(bookCategory);
                    break;
            }
        }

        return bookEntity;
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