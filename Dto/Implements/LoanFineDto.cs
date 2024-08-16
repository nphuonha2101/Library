using Library.Entities;
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

    [SwaggerSchema("Id của phiếu mượn")]
    public long LoanId { get; set; }
    [SwaggerSchema("Số tiền phạt")]
    public double Amount { get; set; }
    [SwaggerSchema("Ngày tạo")]
    public DateTime CreateDate { get; set; }
    [SwaggerSchema("Trạng thái thanh toán")]
    public string PaymentStatus { get; set; } = null!;
    public IEntity ToEntity()
    {
        throw new NotImplementedException();
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
    
}