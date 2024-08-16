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

    public Task<List<Author>> GetAuthorsAsync(long bookId)
    {
        var query = from author in AppDbContext.Authors
            join bookAuthor in AppDbContext.BookAuthors on author.Id equals bookAuthor.AuthorId
            where bookAuthor.BookId == bookId
            select author;

        return query.ToListAsync();
    }

    public Task<List<Category>> GetCategoriesAsync(long bookId)
    {
        var query = from category in AppDbContext.Categories
            join bookCategory in AppDbContext.BookCategories on category.Id equals bookCategory.CategoryId
            where bookCategory.BookId == bookId
            select category;
        
        return query.ToListAsync();
    }

    public Task<List<Book>> GetBooksByCategoryAsync(int categoryId)
    {
        var query = from book in AppDbContext.Books
            where book.BookCategories.Any(bookCategory => bookCategory.CategoryId == categoryId)
            select book;

        return query.ToListAsync();
    }
}