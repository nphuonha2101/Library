using System.ComponentModel.DataAnnotations;
using Library.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Dto.Implements;

public class CategoryDto : IDto
{
    public CategoryDto(string name, string description)
    {
        Name = name;
        Description = description;
    }

    [Required]
    [SwaggerSchema("Tên thể loại")]
    public string Name { get; set; } = null!;

    [Required]
    [SwaggerSchema("Mô tả thể loại")]
    public string Description { get; set; }

    public IEntity ToEntity()
    {
        return new Category(Name, Description);
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}