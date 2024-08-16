using System.ComponentModel.DataAnnotations;
using Library.Entities;
using Library.Entities.Implements;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Dto.Implements;

public class BookDto : IDto
{
    public BookDto(string title, string isbn, string description, DateTime importedDate, int quantity)
    {
        Title = title;
        Isbn = isbn;
        Description = description;
        ImportedDate = importedDate;
        Quantity = quantity;
    }

    [Required]
    [SwaggerSchema("Tiêu đề sách")]
    public string Title { get; set; } = null!;

    [Required]
    [SwaggerSchema("ISBN của sách")]
    public string Isbn { get; set; } = null!;

    [Required]
    [SwaggerSchema("Mô tả sách")]
    public string Description { get; set; } = null!;

    [Required]
    [SwaggerSchema("Ngày nhập sách")]
    public DateTime ImportedDate { get; set; }

    [Required]
    [SwaggerSchema("Số lượng sách")]
    public int Quantity { get; set; }

    [SwaggerSchema("Danh sách id của các thể loại")]
    public List<long>? CategoryIds { get; set; } = new();

    [SwaggerSchema("Danh sách id của các tác giả")]
    public List<long>? AuthorIds { get; set; } = new();

    public IEntity ToEntity()
    {
        return new Book(
            Title, Isbn, Description, ImportedDate, Quantity
        );
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        var book = new Book(Title, Isbn, Description, ImportedDate, Quantity);
        var relatedEntities = new List<IEntity>();

        if (AuthorIds != null)
            foreach (var authorId in AuthorIds)
                relatedEntities.Add(new BookAuthor(authorId, book.Id));

        if (CategoryIds != null)
            foreach (var categoryId in CategoryIds)
                relatedEntities.Add(new BookCategory(categoryId, book.Id));

        return (book, relatedEntities);
    }

    public void SetIds(List<long> authorIds, List<long> categoryIds)
    {
        CategoryIds = categoryIds;
        AuthorIds = authorIds;
    }
}