using Library.Entities;

namespace Library.Dto.Implements;

public class UserDto : IDto
{
    public UserDto(string fullName, string username, string email, string address, string password, DateTime dob, bool isAdmin)
    {
        FullName = fullName;
        Username = username;
        Email = email;
        Address = address;
        Password = password;
        Dob = dob;
        IsAdmin = isAdmin;
    }

    private string FullName { get; set; } = null!;
    private string Username { get; set; } = null!;
    private string Email { get; set; } = null!;
    private string Address { get; set; } = null!;
    private string Password { get; set; } = null!;
    private DateTime Dob { get; set; }
    private bool IsAdmin { get; set; }

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