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

    public BookRepository(ApplicationDbContext appDbContext, IBookAuthorRepository bookAuthorRepository,
        IBookCategoryRepository bookCategoryRepository) : this(appDbContext)
    {
        _bookAuthorRepository = bookAuthorRepository;
        _bookCategoryRepository = bookCategoryRepository;
    }

    public async Task<List<Book>?> GetBooksByAuthorAsync(long authorId)
    {
        var query = from book in AppDbContext.Books
            where book.BookAuthors.Any(bookAuthor => bookAuthor.AuthorId == authorId)
            select book;

        return await query.ToListAsync();
    }

    public async Task<Book?> AddAsync(BookDto dto)
    {
        var (book, intermediateEntities) = dto.ToEntities();

        var bookEntity = (Book)book;

        await AddAsync(bookEntity);

        foreach (var entity in intermediateEntities)
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

        return bookEntity;
    }

    public async Task<Book?> UpdateAsync(long id, BookDto bookDto)
    {
        
        var (book, intermediateEntities) = bookDto.ToEntities();
        
        var existingBook = await Entities
            .Include(b => b.BookAuthors)
            .Include(b => b.BookCategories)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (existingBook == null)
            throw new Exception("Book not found with id: " + id);

        // Update main book properties
        // AppDbContext.Entry(existingBook).CurrentValues.SetValues(book);
        // AppDbContext.Entry(existingBook).Property(b => b.Id).IsModified = false;
        
        var bookEntity = (Book)book;
        
        existingBook.Title = bookEntity.Title;
        existingBook.Description = bookEntity.Description;
        existingBook.Isbn = bookEntity.Isbn;
        existingBook.ImportedDate = bookEntity.ImportedDate;
        existingBook.Quantity = bookEntity.Quantity;
        existingBook.BookImage = bookEntity.BookImage;
        
        // Update authors
        var newAuthors = intermediateEntities.OfType<BookAuthor>().ToList();
        foreach (var newAuthor in newAuthors)
        {
                newAuthor.BookId = id;
        }
        
        foreach (var newAuthor in newAuthors)
        {
            var existingAuthor = existingBook.BookAuthors.FirstOrDefault(ba => ba.AuthorId == newAuthor.AuthorId);
            if (existingAuthor == null)
            {
                
                existingBook.BookAuthors.Add(newAuthor);
            }
        }

        foreach (var existingAuthor in existingBook.BookAuthors.ToList())
        {
            if (newAuthors.All(na => na.AuthorId != existingAuthor.AuthorId))
            {
                existingBook.BookAuthors.Remove(existingAuthor);
            }
        }

        // Update categories
        var newCategories = intermediateEntities.OfType<BookCategory>().ToList();

        foreach (var newCategory in newCategories)
        {
            newCategory.BookId = id;
        }
        
        foreach (var newCategory in newCategories)
        {
            var existingCategory = existingBook.BookCategories.FirstOrDefault(bc => bc.CategoryId == newCategory.CategoryId);
            if (existingCategory == null)
            {
                existingBook.BookCategories.Add(newCategory);
            }
        }

        foreach (var existingCategory in existingBook.BookCategories.ToList())
        {
            if (newCategories.All(nc => nc.CategoryId != existingCategory.CategoryId))
            {
                existingBook.BookCategories.Remove(existingCategory);
            }
        }

        await AppDbContext.SaveChangesAsync();
        return existingBook;
    }

    public async Task<List<Author>?> GetAuthorsAsync(long bookId)
    {
        var query = from author in AppDbContext.Authors
            join bookAuthor in AppDbContext.BookAuthors on author.Id equals bookAuthor.AuthorId
            where bookAuthor.BookId == bookId
            select author;

        return await query.ToListAsync();
    }

    public async Task<List<Category>?> GetCategoriesAsync(long bookId)
    {
        var query = from category in AppDbContext.Categories
            join bookCategory in AppDbContext.BookCategories on category.Id equals bookCategory.CategoryId
            where bookCategory.BookId == bookId
            select category;

        return await query.ToListAsync();
    }

    public async Task<List<Book>?> GetBooksByCategoryAsync(long categoryId)
    {
        var query = from book in AppDbContext.Books
            where book.BookCategories.Any(bookCategory => bookCategory.CategoryId == categoryId)
            select book;

        return await query.ToListAsync();
    }

    public async Task<List<Book>?> GetBooksByTitleAsync(string title)
    {
        return await AppDbContext.Books.Where(b => b.Title.Contains(title)).ToListAsync();
    }

    public override async Task<bool> DeleteAsync(long id)
    {
        var existingBook = await Entities
            .Include(b => b.BookAuthors)
            .Include(b => b.BookCategories)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (existingBook == null)
            return false;

        // Remove related authors
        foreach (var author in existingBook.BookAuthors.ToList())
        {
            AppDbContext.BookAuthors.Remove(author);
        }

        // Remove related categories
        foreach (var category in existingBook.BookCategories.ToList())
        {
            AppDbContext.BookCategories.Remove(category);
        }

        // Remove the book
        Entities.Remove(existingBook);

        await AppDbContext.SaveChangesAsync();
        return true;
    }
}