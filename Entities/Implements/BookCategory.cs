using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Entities.Implements;

public class BookCategory
{
    [Key, Column(Order = 0)]
    [ForeignKey("Book")]
    public long BookId { get; set; } 

    [Key, Column(Order = 1)]
    [ForeignKey("Category")]
    public long CategoryId { get; set; }

    public virtual Book Book { get; set; }
    public virtual Category Category { get; set; }
}
