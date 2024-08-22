using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Library.Dto;
using Library.Dto.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Entities.Implements;

[Table("loan_details")]
public class LoanDetail : IEntity
{
    public LoanDetail(long loanId, long bookId, int quantity, DateTime dueDate)
    {
        LoanId = loanId;
        BookId = bookId;
        Quantity = quantity;
        DueDate = dueDate;
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [Column("loan_id")] public long LoanId { get; set; }
    [JsonIgnore] [ForeignKey("LoanId")] public Loan? Loan { get; set; } = null!;

    [Required] [Column("book_id")] public long BookId { get; set; }
    [ForeignKey("BookId")] public Book? Book { get; set; } = null!;

    [Required] [Column("quantity")] public int Quantity { get; set; }
    [Required] [Column("due_date")] public DateTime DueDate { get; set; }
    [Column("return_date")] public DateTime? ReturnDate { get; set; }

    public IDto ToDto()
    {
        return new LoanDetailDto(LoanId, BookId, Quantity, DueDate);
    }

    public long GetId()
    {
        return Id;
    }
}