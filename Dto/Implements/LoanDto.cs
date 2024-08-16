using Library.Entities;

namespace Library.Dto.Implements;

public class LoanDto : IDto
{
    public LoanDto(long userId, DateTime loanDate)
    {
        UserId = userId;
        LoanDate = loanDate;
    }

    public long UserId { get; set; }
    public DateTime LoanDate { get; set; }
    public IEntity ToEntity()
    {
        throw new NotImplementedException();
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}