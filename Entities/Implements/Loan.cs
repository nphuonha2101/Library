using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Implements;

[Table("loans")]
public class Loan : IEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [Column("user_id")] public long UserId { get; set; }

    [Required] [ForeignKey("UserId")] public User User { get; set; } = null!;

    [Required] [Column("loan_date")] public DateTime LoanDate { get; set; }

    [Column("loan_fine_id")] public long LoanFineId;
    [ForeignKey("LoanFineId")] public virtual LoanFine LoanFine { get; set; } = null!;
}