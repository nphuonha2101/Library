using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Implements;

[Table("authors")]
public class Author : IEntity
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required] [StringLength(255)]
    [Column("full_name")]
    public string FullName { get; set; }
    [Required]
    [Column("dob")]
    public DateTime Dob { get; set; }
    [Required] [StringLength(255)]
    [Column("description")]
    public string Description { get; set; }
    public List<BookAuthor> BookAuthors { get; set; }

}