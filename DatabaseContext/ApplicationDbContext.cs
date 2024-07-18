using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DatabaseContext;

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
