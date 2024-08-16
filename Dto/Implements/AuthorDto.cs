using Library.Entities;
using Library.Entities.Implements;

namespace Library.Dto.Implements;

public class AuthorDto : IDto
{
    public AuthorDto(string fullName, DateTime dob, string description)
    {
        FullName = fullName;
        Dob = dob;
        Description = description;
    }

    public string FullName { get; set; } = null!;
    public DateTime Dob { get; set; }
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