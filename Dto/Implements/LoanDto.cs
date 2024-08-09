using Library.Entities;

namespace Library.Dto.Implements;

public class LoanDto : IDto
{
    
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