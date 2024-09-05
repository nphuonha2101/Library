using System.ComponentModel.DataAnnotations;
using Library.Entities;
using Library.Entities.Implements;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Dto.Implements;

public class LoanDetailDto : IDto
{
    public LoanDetailDto(long loanId, long bookId, int quantity, DateTime dueDate)
    {
        LoanId = loanId;
        BookId = bookId;
        Quantity = quantity;
        DueDate = dueDate;
    }

    [Required]
    [SwaggerSchema("Id của phiếu mượn")]
    public long LoanId { get; set; }

    [Required]
    [SwaggerSchema("Id của sách")]
    public long BookId { get; set; }

    [Required]
    [SwaggerSchema("Số lượng sách mượn")]
    public int Quantity { get; set; }

    [Required]
    [SwaggerSchema("Ngày hết hạn")]
    public DateTime DueDate { get; set; }
    
    [SwaggerSchema("Ngày trả sách")]
    public DateTime ReturnDate { get; set; }

    public IEntity ToEntity()
    {
        return new LoanDetail(LoanId, BookId, Quantity, DueDate);
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}