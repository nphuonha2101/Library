using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class BookReviewService(IBookReviewRepository _bookReviewRepository) : IBookReviewService
{
    public List<BookReview>? GetAll()
    {
        return _bookReviewRepository.GetAllAsync().Result;
    }

    public BookReview? GetById(long id)
    {
        return _bookReviewRepository.GetByIdAsync(id).Result;
    }

    public BookReview? Add(BookReview entity)
    {
        return _bookReviewRepository.AddAsync(entity).Result;
    }

    public BookReview? Update(long id, BookReview entity)
    {
        return _bookReviewRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(long id)
    {
        return _bookReviewRepository.DeleteAsync(id).Result;
    }

    public List<BookReview>? GetByBookId(long bookId)
    {
        return _bookReviewRepository.GetByBookId(bookId).Result;
    }

    public List<BookReview>? GetByUserId(long userId)
    {
        return _bookReviewRepository.GetByUserId(userId).Result;
    }
}