using Library.Entities;

namespace Library.Dto.Implements;

public class LoanDetailDto : IDto
{
    public long LoanId { get; set; }
    public long BookId { get; set; }
    public int Quantity { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public IEntity ToEntity()
    {
        throw new NotImplementedException();
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}