using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Category
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; }

    [StringLength(500)]
    [Column("description")]
    public string Description { get; set; }

    public virtual ICollection<BookCategory> BookCategories { get; set; }

    public Category()
    {
        BookCategories = new HashSet<BookCategory>();
    }
}
