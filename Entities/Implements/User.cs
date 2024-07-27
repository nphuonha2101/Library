using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [StringLength(100)]
    [Column("full_name")]
    public string FullName { get; set; }

    [Required]
    [StringLength(50)]
    [Column("username")]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    [Column("email")]
    public string Email { get; set; }

    [Required]
    [Column("dob")]
    public DateTime Dob { get; set; }

    [StringLength(200)]
    [Column("address")]
    public string Address { get; set; }
    [Column("is_admin")]
    public bool IsAdmin { get; set; }

    public virtual ICollection<Loan> Loans { get; set; }

    public User()
    {
        Loans = new HashSet<Loan>();
    }
}
