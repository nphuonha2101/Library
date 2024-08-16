using System.ComponentModel.DataAnnotations;
using Library.Entities;
using Library.Entities.Implements;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Dto.Implements;

public class AuthorDto : IDto
{
    public AuthorDto(string fullName, DateTime dob, string description)
    {
        FullName = fullName;
        Dob = dob;
        Description = description;
    }

    [Required]
    [SwaggerSchema("Tên tác giả")]
    public string FullName { get; set; } = null!;

    [Required]
    [SwaggerSchema("Ngày sinh tác giả")]
    public DateTime Dob { get; set; }

    [Required]
    [SwaggerSchema("Mô tả tác giả")]
    public string Description { get; set; } = null!;

    public IEntity ToEntity()
    {
        return new Author(
            FullName,
            Dob,
            Description
        );
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}