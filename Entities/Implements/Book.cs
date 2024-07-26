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
    public long  Id { get; set; }
    [Required]
    [StringLength(50)]
    [Column("isbn")]
    public string isBn { get; set; }
    [Required]
    [StringLength(255)]
    [Column("description")]
    public string Description { get; set; }
    [Column("import_date")]
    public DateTime ImportedDate { get; set; }
    [Required]
    [Column("quantity")]
    public int Quantity { get; set; }
    public List<BookAuthor> BookAuthors { get; set; }
    public List<BookCategory> BookCategories { get; set; }
}