using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Implements;

public class LoanDetail
{
    [Key]
    [ForeignKey("LoanId")]
    [Column("loan_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long LoanId { get; set; }
    public Loan Loan { get; set; }
    [Key]
    [Column("book_id")]
    public long BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }
    [Required]
    [Column("quantity")]
    public int Quantity { get; set; }
    [Required]
    [Column("due_date")]
    public DateTime DueDate { get; set; }
    [Required]
    [Column("return_date")]
    public DateTime ReturnDate { get; set; }
}
