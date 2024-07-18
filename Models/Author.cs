using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models;

[Table("authors")]
public class Author
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    private long Id { get; set; }

}