using System.ComponentModel.DataAnnotations;
using Library.Entities;
using Library.Entities.Implements;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Dto.Implements;

public class LoanDto : IDto
{
    public LoanDto(long userId, DateTime loanDate, long? loanFineId, DateTime dueDate, DateTime? returnDate)
    {
        UserId = userId;
        LoanDate = loanDate;
        LoanFineId = loanFineId;
        DueDate = dueDate;
        if (returnDate != null) ReturnDate = returnDate;
    }

    [Required]
    [SwaggerSchema("Id của người mượn")]
    public long UserId { get; set; }

    [SwaggerSchema("Ngày mượn")]
    [Required]
    public DateTime LoanDate { get; set; }
    
    [SwaggerSchema("Ngày hết hạn")]
    [Required]
    public DateTime DueDate { get; set; }
    
    [SwaggerSchema("Ngày trả sách")]
    public DateTime? ReturnDate { get; set; }
    
    [SwaggerSchema("Id của phiếu phạt")] public long? LoanFineId { get; set; }

    public IEntity ToEntity()
    {
        return new Loan(LoanFineId, UserId, LoanDate, DueDate, ReturnDate);
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}