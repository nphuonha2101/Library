using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library.Entities.Implements;

[Table("book_author")]
[PrimaryKey(nameof(BookId), nameof(AuthorId))]
public class BookAuthor : IEntity
{
    public BookAuthor(long bookId, long authorId)
    {
        this.BookId = bookId;
        this.AuthorId = authorId;
    }
    [Key] [Column("book_id")] public long BookId { get; set; }

    [ForeignKey("BookId")] public virtual Book Book { get; set; } = null!;

    [Key] [Column("author_id")] public long AuthorId { get; set; }
    [ForeignKey("AuthorId")] public virtual Author Author { get; set; } = null!;
}