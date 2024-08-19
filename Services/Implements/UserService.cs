using Library.Data.Repositories.Interfaces;
using Library.Dto.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class UserService(IUserRepository userRepository) : IUserService
{
    public List<User>? GetAll()
    {
        return userRepository.GetAllAsync().Result;
    }

    public User? GetById(long id)
    {
        return userRepository.GetByIdAsync(id).Result;
    }

    public User? Add(User entity)
    {
        return userRepository.AddAsync(entity).Result;
    }

    public User? Update(long id, User entity)
    {
        return userRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(long id)
    {
        return userRepository.DeleteAsync(id).Result;
    }

    public User? Login(string usernameOrEmail, string password)
    {
        return userRepository.LoginAsync(usernameOrEmail, password).Result;
    }

    public User? Register(UserDto user)
    {
        return userRepository.AddAsync((User)user.ToEntity()).Result;
    }
}