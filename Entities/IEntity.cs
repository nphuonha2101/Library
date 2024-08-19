using Library.Dto;

namespace Library.Entities;

public interface IEntity
{
    IDto ToDto();
    long GetId();
}