using Library.Dto.Implements;
using Library.Entities.Implements;

namespace Library.Services.Interfaces;

public interface IUserService : IService<User>
{
    User Login(string usernameOrEmail, string password);
    User Register(UserDto user);

}