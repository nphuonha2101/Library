using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Library.Dto;
using Library.Dto.Implements;

namespace Library.Entities.Implements;

[Table("book_reviews")]
public class BookReview : IEntity
{
    public BookReview(long bookId, long userId, string? title, string? review, int rating, DateTime createdAt)
    {
        BookId = bookId;
        UserId = userId;
        Title = title ?? "";
        Review = review ?? "";
        Rating = rating;
        CreatedAt = createdAt;
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("book_id")] public long BookId { get; set; }
    [ForeignKey("BookId")] public virtual Book? Book { get; set; }

    [Required]
    [Column("user_id")] public long UserId { get; set; }
    [ForeignKey("UserId")] public virtual User? User { get; set; }
    
    [StringLength(255)]
    [Column("title")]
    public string Title { get; set; }

    [StringLength(255)] [Column("review")] public string Review { get; set; }

    [Column("rating")] [Required] public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }

    
    public IDto ToDto()
    {
        return new BookReviewDto(BookId, UserId, Title, Review, Rating, CreatedAt);
    }

    public long GetId()
    {
        return Id;
    }
}