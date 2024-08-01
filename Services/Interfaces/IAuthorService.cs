using Library.Entities.Implements;

namespace Library.Services.Interfaces;

public interface IAuthorService: IService<Author>
{
    List<Author> GetAll();
    Author GetById(int id);

}