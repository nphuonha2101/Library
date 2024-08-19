using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public List<Author> GetAll()
    {
        return _authorRepository.GetAllAsync().Result;
    }

    public Author GetById(long id)
    {
        return _authorRepository.GetByIdAsync(id).Result;
    }

    public Author Add(Author author)
    {
        return _authorRepository.AddAsync(author).Result;
    }

    public bool Update(long id, Author author)
    {
        return _authorRepository.UpdateAsync(id, author).Result;
    }

    public bool Delete(long id)
    {
        return _authorRepository.DeleteAsync(id).Result;
    }
}