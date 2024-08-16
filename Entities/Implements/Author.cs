using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Library.Dto;
using Library.Dto.Implements;

namespace Library.Entities.Implements;

[Table("authors")]
public class Author : IEntity
{
    public Author(string fullName, DateTime dob, string description)
    {
        this.FullName = fullName;
        this.Dob = (DateTime)dob;
        this.Description = description;
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [StringLength(255)]
    [Column("full_name")]
    public string FullName { get; set; } = null!;

    [Required]
    [Column("dob")] public DateTime Dob { get; set; }

    [Required]
    [StringLength(255)]
    [Column("description")]
    public string Description { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new HashSet<BookAuthor>();

    public IDto ToDto()
    {
        return new AuthorDto(FullName, Dob, Description);

    }
}