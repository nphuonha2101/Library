using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Implements;
[Table("loans")]
public class Loan
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long  Id { get; set; }
    [Required]
    [ForeignKey("UserId")]
    [Column("user_id")]
    public long UserId { get; set; }
    public User User { get; set; }
    [Required]
    [Column("loan_date")]
    public DateTime LoanDate { get; set; }
    public LoanFine LoanFine { get; set; }
}