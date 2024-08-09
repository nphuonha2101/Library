using Library.Entities;

namespace Library.Dto;

public interface IDto
{
    IEntity ToEntity();

    (IEntity, List<IEntity>) ToEntities();
}