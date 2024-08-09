using Library.Entities;

namespace Library.Dto.Implements;

public class CategoryDto : IDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public IEntity ToEntity()
    {
        throw new NotImplementedException();
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}