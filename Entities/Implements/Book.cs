using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Library.Dto;
using Library.Dto.Implements;

namespace Library.Entities.Implements;

[Table("books")]
public class Book : IEntity
{
    public Book(string title, string isbn, string description, DateTime importedDate, int quantity)
    {
        Title = title;
        Isbn = isbn;
        Description = description;
        ImportedDate = importedDate;
        Quantity = quantity;
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    [Required]
    [StringLength(255)]
    [Column("title")]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [Column("isbn")]
    public string Isbn { get; set; } = null!;

    [Required]
    [StringLength(255)]
    [Column("description")]
    public string Description { get; set; } = null!;

    [Required] [Column("import_date")] public DateTime ImportedDate { get; set; }

    [Required] [Column("quantity")] public int Quantity { get; set; }

    [JsonIgnore] public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new HashSet<BookAuthor>();
    [JsonIgnore] public virtual ICollection<BookCategory> BookCategories { get; set; } = new HashSet<BookCategory>();

    [NotMapped] public List<AuthorDto> Authors { get; set; } = new();
    [NotMapped] public List<CategoryDto> Categories { get; set; } = new();


    public IDto ToDto()
    {
        return new BookDto(Title, Isbn, Description, ImportedDate, Quantity);
    }
}