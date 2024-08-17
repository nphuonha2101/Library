using System.ComponentModel.DataAnnotations;
using Library.Entities;
using Library.Entities.Implements;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Dto.Implements;

public class LoanFineDto : IDto
{
    public LoanFineDto(long loanId, double amount, DateTime createDate, string paymentStatus)
    {
        LoanId = loanId;
        Amount = amount;
        CreateDate = createDate;
        PaymentStatus = paymentStatus;
    }

    [Required]
    [SwaggerSchema("Id của phiếu mượn")]
    public long LoanId { get; set; }

    [SwaggerSchema("Số tiền phạt")]
    [Required]
    public double Amount { get; set; }

    [SwaggerSchema("Ngày tạo")] [Required] public DateTime CreateDate { get; set; }

    [SwaggerSchema("Trạng thái thanh toán")]
    [Required]
    public string PaymentStatus { get; set; } = null!;

    public IEntity ToEntity()
    {
        return new LoanFine(LoanId, Amount, CreateDate, PaymentStatus);
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}