using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Implements;

[Table("authors")]
public class Author : IEntity
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    private long Id { get; set; }

}