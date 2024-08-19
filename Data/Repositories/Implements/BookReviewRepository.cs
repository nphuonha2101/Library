using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class BookReviewRepository(ApplicationDbContext appDbContext)
    : Repository<BookReview>(appDbContext), IBookReviewRepository
{
    public async Task<List<BookReview>?> GetByBookId(long bookId)
    {
        var query = from bookReview in AppDbContext.BookReviews
            where bookReview.BookId == bookId
            select bookReview;
        
        return await query.ToListAsync();
    }

    public async Task<List<BookReview>?> GetByUserId(long userId)
    {
        var query = from bookReview in AppDbContext.BookReviews
            where bookReview.UserId == userId
            select bookReview;
        
        return await query.ToListAsync();
    }
}