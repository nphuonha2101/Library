using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.DatabaseContext;

/**
 * Application database context
 * This class is used to configure the database and entities
 */
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<LoanDetail> LoanDetails { get; set; }
    public DbSet<LoanFine> LoanFines { get; set; }
    public DbSet<User> Users { get; set; }
}