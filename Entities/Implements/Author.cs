using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Implements;

[Table("authors")]
public class Author : IEntity
{
    public Author(string fullName, DateTime? dob, string description)
    {
        this.FullName = fullName;

        if (dob != null)
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

    [Column("dob")] public DateTime Dob { get; set; }

    [Required]
    [StringLength(255)]
    [Column("description")]
    public string Description { get; set; } = null!;

    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new HashSet<BookAuthor>();
}