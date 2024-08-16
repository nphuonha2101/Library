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

    [SwaggerSchema("Tên tác giả")] public string FullName { get; set; } = null!;

    [SwaggerSchema("Ngày sinh tác giả")]
    public DateTime Dob { get; set; }

    [SwaggerSchema("Mô tả tác giả")]
    public string Description { get; set; } = null!;

    public IEntity ToEntity()
    {
        return new Author(
            this.FullName,
            this.Dob,
            Description
        );
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}