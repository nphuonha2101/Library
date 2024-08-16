using Library.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Dto.Implements;

public class UserDto : IDto
{
    public UserDto(string fullName, string username, string email, string address, string password, DateTime dob,
        bool isAdmin)
    {
        FullName = fullName;
        Username = username;
        Email = email;
        Address = address;
        Password = password;
        Dob = dob;
        IsAdmin = isAdmin;
    }

    [SwaggerSchema("Họ và tên")] private string FullName { get; } = null!;

    [SwaggerSchema("Tên đăng nhập")] private string Username { get; } = null!;

    [SwaggerSchema("Email")] private string Email { get; } = null!;

    [SwaggerSchema("Địa chỉ")] private string Address { get; } = null!;

    [SwaggerSchema("Mật khẩu")] private string Password { get; } = null!;

    [SwaggerSchema("Ngày sinh")] private DateTime Dob { get; }

    [SwaggerSchema("Quyền admin")] private bool IsAdmin { get; }

    public IEntity ToEntity()
    {
        return new User
        (
            FullName,
            Username,
            Email,
            Password,
            Dob,
            Address,
            IsAdmin
        );
    }

    public (IEntity, List<IEntity>) ToEntities()
    {
        throw new NotImplementedException();
    }
}