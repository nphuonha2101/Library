using Library.Entities;

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

    public long LoanId { get; set; }
    public double Amount { get; set; }
    public DateTime CreateDate { get; set; }
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