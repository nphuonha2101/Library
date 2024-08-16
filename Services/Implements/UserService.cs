using Library.Data.Repositories.Interfaces;
using Library.Dto.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<User> GetAll()
    {
        return _userRepository.GetAllAsync().Result;
    }

    public User GetById(int id)
    {
        return _userRepository.GetByIdAsync(id).Result;
    }

    public User Add(User entity)
    {
        return _userRepository.AddAsync(entity).Result;
    }

    public bool Update(int id, User entity)
    {
        return _userRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(int id)
    {
        return _userRepository.DeleteAsync(id).Result;
    }

    public User Login(string usernameOrEmail, string password)
    {
        return _userRepository.LoginAsync(usernameOrEmail, password).Result;
    }

    public User Register(UserDto user)
    {
        return _userRepository.AddAsync((User)user.ToEntity()).Result;
    }
}