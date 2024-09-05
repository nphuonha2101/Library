using Library.Entities.Implements;

namespace Library.Data.Repositories.Interfaces;

public interface IBookReviewRepository: IRepository<BookReview>
{
    Task<List<BookReview>?> GetByBookId(long bookId);
    Task<List<BookReview>?> GetByUserId(long userId);
}