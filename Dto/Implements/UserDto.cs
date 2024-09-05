using System.ComponentModel.DataAnnotations;
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

    [Required]
    [SwaggerSchema("Họ và tên")] public string FullName { get; } = null!;

    [Required]
    [SwaggerSchema("Tên đăng nhập")] public string Username { get; } = null!;

    [Required]
    [SwaggerSchema("Email")] public string Email { get; } = null!;

    [Required]
    [SwaggerSchema("Địa chỉ")] public string Address { get; } = null!;

    [Required]
    [SwaggerSchema("Mật khẩu")] public string Password { get; } = null!;

    [Required]
    [SwaggerSchema("Ngày sinh")] public DateTime Dob { get; }

    [Required]
    [SwaggerSchema("Quyền admin")] public bool IsAdmin { get; }

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