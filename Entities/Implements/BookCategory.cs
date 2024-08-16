using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Dto;
using Library.Dto.Implements;
using Library.Entities;
using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

[Table("book_category")]
[PrimaryKey(nameof(BookId), nameof(CategoryId))]
public class BookCategory : IEntity
{
    public BookCategory(long categoryId, long bookId)
    {
        CategoryId = categoryId;
        BookId = bookId;
    }

    [Key] [Column("book_id")] public long BookId { get; set; }
    [Key] [Column("category_id")] public long CategoryId { get; set; }
    [ForeignKey("BookId")] public virtual Book Book { get; set; } = null!;
    [ForeignKey("CategoryId")] public virtual Category Category { get; set; } = null!;
    public IDto ToDto()
    {
        return new BookCategoryDto(BookId, CategoryId);
    }
}