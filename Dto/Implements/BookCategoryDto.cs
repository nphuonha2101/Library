using Library.Entities;

namespace Library.Dto.Implements;

public class BookCategoryDto: IDto
{
    public long BookId { get; set; }
    public long CategoryId { get; set; }
    public IEntity ToEntity()
    {
        throw new NotImplementedException();
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}