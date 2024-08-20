using System.ComponentModel.DataAnnotations;
using Library.Entities;
using Library.Entities.Implements;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Dto.Implements;

public class BookReviewDto
    : IDto
{
    public BookReviewDto(long bookId, long userId, string? title = null, string? review = null, int rating = 0, DateTime? createdAt = null)
    {
        BookId = bookId;
        UserId = userId;
        Title = title ?? string.Empty;
        Review = review ?? string.Empty;
        Rating = rating;
        CreatedAt = createdAt ?? DateTime.Now;
    }

    [Required]
    [SwaggerSchema("Id của sách")]
    public long BookId { get; set; }

    [Required]
    [SwaggerSchema("Id của người dùng")]
    public long UserId { get; set; }

    [SwaggerSchema("Tiêu đề")] public string Title { get; set; }
    [SwaggerSchema("Nhận xét")] public string Review { get; set; }
    [Required] [SwaggerSchema("Đánh giá")] public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }

    
    public Book? Book { get; set; }
    public User? User { get; set; }

    public IEntity ToEntity()
    {
        return new BookReview(
            BookId,
            UserId,
            Title,
            Review,
            Rating,
            CreatedAt
        );
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}