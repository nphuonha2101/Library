using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Dto;
using Library.Dto.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Entities.Implements;

[Table("loan_details")]
[PrimaryKey(nameof(LoanId), nameof(BookId))]
public class LoanDetail : IEntity
{
    public LoanDetail(long loanId, long bookId, int quantity, DateTime dueDate, DateTime returnDate)
    {
        LoanId = loanId;
        BookId = bookId;
        Quantity = quantity;
        DueDate = dueDate;
        ReturnDate = returnDate;
    }

    [Key] [Column("loan_id")] public long LoanId { get; set; }

    [ForeignKey("LoanId")] public Loan Loan { get; set; } = null!;
    [Key] [Column("book_id")] public long BookId { get; set; }
    [ForeignKey("BookId")] public Book Book { get; set; } = null!;

    [Required] [Column("quantity")] public int Quantity { get; set; }
    [Required] [Column("due_date")] public DateTime DueDate { get; set; }
    [Required] [Column("return_date")] public DateTime ReturnDate { get; set; }

    public IDto ToDto()
    {
        return new LoanDetailDto(LoanId, BookId, Quantity, DueDate, ReturnDate);
    }

    public long GetId()
    {
        throw new NotImplementedException();
    }
}