using Library.Entities;
using Library.Entities.Implements;

namespace Library.Dto.Implements;

public class BookDto : IDto
{
    public BookDto(string isbn, string description, DateTime importedDate, int quantity)
    {
        Isbn = isbn;
        Description = description;
        ImportedDate = importedDate;
        Quantity = quantity;
    }

    public string Isbn { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime ImportedDate { get; set; }
    public int Quantity { get; set; }
    public List<long>? CategoryIds { get; set; } = new List<long>();
    public List<long>? AuthorIds { get; set; } = new List<long>();

    public IEntity ToEntity()
    {
        return new Book(
            Isbn, Description, ImportedDate, Quantity
        );
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        var book = new Book(Isbn, Description, ImportedDate, Quantity);
        var relatedEntities = new List<IEntity>();

        if (AuthorIds != null)
        {
            foreach (var authorId in AuthorIds)
            {
                relatedEntities.Add(new BookAuthor(authorId, book.Id));
            }
        }

        if (CategoryIds != null)
        {
            foreach (var categoryId in CategoryIds)
            {
                relatedEntities.Add(new BookCategory(categoryId, book.Id));
            }
        }

        return (book, relatedEntities);
    }
    
}