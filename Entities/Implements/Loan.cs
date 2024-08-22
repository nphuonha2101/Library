using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Library.Dto;
using Library.Dto.Implements;

namespace Library.Entities.Implements;

[Table("loans")]
public class Loan : IEntity
{
    public Loan(long? loanFineId, long userId, DateTime loanDate, DateTime dueDate, DateTime? returnDate)
    {
        LoanFineId = loanFineId;
        UserId = userId;
        LoanDate = loanDate;
        DueDate = dueDate;
        if (returnDate != null) ReturnDate = returnDate;
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [Column("user_id")] public long UserId { get; set; }

    [Required] [ForeignKey("UserId")] public User User { get; set; } = null!;

    [Required] [Column("loan_date")] public DateTime LoanDate { get; set; }
    [Required] [Column("due_date")] public DateTime DueDate { get; set; }
    [Column("return_date")] public DateTime? ReturnDate { get; set; }

    [Column("loan_fine_id")] public long? LoanFineId;

    [JsonIgnore]
    [ForeignKey("LoanFineId")]
    public virtual LoanFine? LoanFine { get; set; } = null!;

    public IDto ToDto()
    {
        return new LoanDto(UserId, LoanDate, LoanFineId, DueDate, ReturnDate);
    }

    public long GetId()
    {
        return Id;
    }
}