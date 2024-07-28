using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Implements;


[Table("book_authors")]
public class BookAuthor : IEntity
{
    [Key]
    [Column("book_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }
    [Key]
    [Column("author_id")]
    public long AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Author Author { get; set; }
}