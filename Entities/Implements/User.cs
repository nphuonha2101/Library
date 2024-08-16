using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Library.Dto;
using Library.Dto.Implements;
using Library.Entities;
using Library.Entities.Implements;

[Table("users")]
public class User : IEntity
{
    public User(string fullName, string username, string email, string password, DateTime dob, string address, bool isAdmin)
    {
        FullName = fullName;
        Username = username;
        Email = email;
        Password = password;
        Dob = dob;
        Address = address;
        IsAdmin = isAdmin;
    }

    [Key] [Column("id")] public long Id { get; set; }

    [Required]
    [StringLength(100)]
    [Column("full_name")]
    public string FullName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [Column("username")]
    public string Username { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Column("email")]
    public string Email { get; set; } = null!;
    
    [Required]
    [StringLength(100)]
    [Column("password")]
    public string Password { get; set; } = null!;

    [Required] [Column("dob")] public DateTime Dob { get; set; }

    [StringLength(200)]
    [Column("address")]
    public string Address { get; set; } = null!;

    [Column("is_admin")] public bool IsAdmin { get; set; }

    [JsonIgnore]
    public virtual ICollection<Loan> Loans { get; set; } = new HashSet<Loan>();

    public IDto ToDto()
    {
        return new UserDto(FullName, Username, Email, Address, Password, Dob, IsAdmin);
    }
}