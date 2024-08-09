using Library.Entities;

namespace Library.Dto.Implements;

public class BookAuthorDto : IDto
{
    public long BookId { get; set; }
    public long AuthorId { get; set; }


    public IEntity ToEntity()
    {
        throw new NotImplementedException();
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}