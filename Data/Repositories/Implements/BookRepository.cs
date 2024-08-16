using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Dto.Implements;
using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class BookRepository(ApplicationDbContext appDbContext) : Repository<Book>(appDbContext), IBookRepository
{
    private readonly IBookAuthorRepository _bookAuthorRepository;
    private readonly IBookCategoryRepository _bookCategoryRepository;

    public BookRepository(ApplicationDbContext appDbContext, IBookAuthorRepository bookAuthorRepository, IBookCategoryRepository bookCategoryRepository) : this(appDbContext)
    {
        _bookAuthorRepository = bookAuthorRepository;
        _bookCategoryRepository = bookCategoryRepository;
    }

    public async Task<List<Book>> GetBooksByAuthorAsync(int authorId)
    {
        var query = from book in AppDbContext.Books
            where book.BookAuthors.Any(bookAuthor => bookAuthor.AuthorId == authorId)
            select book;

        return await query.ToListAsync();
    }
    
    public async Task<Book> AddAsync(BookDto dto) 
    {
        var (book, intermediateEntities) = dto.ToEntities();

        var bookEntity = (Book)book;

        await this.AddAsync(bookEntity); 

        foreach (var entity in intermediateEntities)
        {
            switch (entity)
            {
                case BookAuthor bookAuthor:
                    bookAuthor.BookId = bookEntity.Id;
                    await _bookAuthorRepository.AddAsync(bookAuthor); 
                    break;

                case BookCategory bookCategory:
                    bookCategory.BookId = bookEntity.Id;
                    await _bookCategoryRepository.AddAsync(bookCategory);
                    break;
            }
        }
        return bookEntity;
    }

}