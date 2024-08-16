using Library.Data.Repositories.Implements;
using Library.Entities.Implements;

namespace Library.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> LoginAsync(string usernameOrEmail, string password);
}