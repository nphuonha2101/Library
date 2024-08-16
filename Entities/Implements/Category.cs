using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Library.Entities;

[Table("categories")]
public class Category : IEntity
{
    [Key] [Column("id")] public long Id { get; init; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    [Column("description")]
    public string Description { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<BookCategory> BookCategories { get; set; } = new HashSet<BookCategory>();
}