using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Services.Implements;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library.Entities.Implements;

[Table("books")]
public class Book : IEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    [Required]
    [StringLength(50)]
    [Column("isbn")]
    public string Isbn { get; set; } = null!;

    [Required]
    [StringLength(255)]
    [Column("description")]
    public string Description { get; set; } = null!;
    [Required]
    [Column("import_date")]
    public DateTime ImportedDate { get; set; }
    [Required]
    [Column("quantity")]
    public int Quantity { get; set; }

    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new HashSet<BookAuthor>();
    public virtual ICollection<BookCategory> BookCategories { get; set; } = new HashSet<BookCategory>();
}