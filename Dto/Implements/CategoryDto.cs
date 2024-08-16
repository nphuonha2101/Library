using Library.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Dto.Implements;

public class CategoryDto : IDto
{
    public CategoryDto(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    [SwaggerSchema("Tên thể loại")]
    public string Name { get; set; } = null!;
    [SwaggerSchema("Mô tả thể loại")]
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