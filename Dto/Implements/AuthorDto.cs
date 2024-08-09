using Library.Entities;
using Library.Entities.Implements;

namespace Library.Dto.Implements;

public class AuthorDto : IDto
{
    private string FullName { get; set; } = null!;
    private DateTime? Dob { get; set; }
    private string Description { get; set; } = null!;
    
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