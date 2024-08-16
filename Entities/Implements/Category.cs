using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Library.Dto;
using Library.Dto.Implements;
using Library.Entities;

[Table("categories")]
public class Category : IEntity
{
    public Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    [Key] [Column("id")] public long Id { get; init; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    [Column("description")]
    public string Description { get; set; } = null!;

    [JsonIgnore] public virtual ICollection<BookCategory> BookCategories { get; set; } = new HashSet<BookCategory>();

    public IDto ToDto()
    {
        return new CategoryDto(Name, Description);
    }
}