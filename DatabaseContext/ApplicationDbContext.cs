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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().ToTable("authors");
        modelBuilder.Entity<Book>().ToTable("books");
     
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
   
    
}
