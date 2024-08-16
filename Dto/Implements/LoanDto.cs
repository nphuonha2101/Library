using Library.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Dto.Implements;

public class LoanDto : IDto
{
    public LoanDto(long userId, DateTime loanDate)
    {
        UserId = userId;
        LoanDate = loanDate;
    }

    [SwaggerSchema("Id của người mượn")]
    public long UserId { get; set; }
    [SwaggerSchema("Ngày mượn")]
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