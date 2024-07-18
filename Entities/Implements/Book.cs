using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Implements;

[Table("books")]
public class Book : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    public long AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Author author { get; set; }
    [Required]
    [StringLength(50)]
    public string isbn { get; set; }
}