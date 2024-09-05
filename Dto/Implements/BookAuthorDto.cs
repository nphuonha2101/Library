using Library.Entities;

namespace Library.Dto.Implements;

public class BookAuthorDto(long bookId, long authorId) : IDto
{
    public long BookId { get; set; } = bookId;
    public long AuthorId { get; set; } = authorId;


    public IEntity ToEntity()
    {
        throw new NotImplementedException();
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}