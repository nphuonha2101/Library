using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Implements;


[Table("book_authors")]
public class BookAuthor : IEntity
{
    [Key]
    [ForeignKey("BookId")]
    [Column("book_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long BookId { get; set; }
    public Book Book { get; set; }
    [ForeignKey("AuthorId")]
    [Key]
    [Column("author_id")]
    public long AuthorId { get; set; }
    public Author Author { get; set; }
}