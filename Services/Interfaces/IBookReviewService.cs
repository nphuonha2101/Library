using Library.Entities.Implements;

namespace Library.Services.Interfaces;

public interface IBookReviewService: IService<BookReview>
{
    List<BookReview>? GetByBookId(long bookId);
    List<BookReview>? GetByUserId(long userId);
}