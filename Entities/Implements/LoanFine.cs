using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Dto;
using Library.Dto.Implements;

namespace Library.Entities.Implements;

[Table("fines")]
public class LoanFine : IEntity
{
    public LoanFine(long loanId, double amount, DateTime createDate, string paymentStatus)
    {
        LoanId = loanId;
        Amount = amount;
        CreateDate = createDate;
        PaymentStatus = paymentStatus;
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [Column("loan_id")] public long LoanId { get; set; }
    [ForeignKey("LoanId")] public Loan? Loan { get; set; } = null!;
    [Required] [Column("amount")] public double Amount { get; set; }
    [Required] [Column("create_date")] public DateTime CreateDate { get; set; }
    [Required] [Column("payment_status")] public string PaymentStatus { get; set; } = null!;

    public IDto ToDto()
    {
        return new LoanFineDto(LoanId, Amount, CreateDate, PaymentStatus);
    }

    public long GetId()
    {
        return Id;
    }
}